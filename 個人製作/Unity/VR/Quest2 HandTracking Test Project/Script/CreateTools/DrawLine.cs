using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3D空間上に 線描画

public class DrawLine : MonoBehaviour
{
    [SerializeField] private Grab grab;

    private HandSign handSign;
    private GameObject CurrentLineObject;

    private LineRenderer render;
    public  LineRenderer GetRenderer() { return render; }

    [HideInInspector] public bool oldRenderEnabled;

    private void Awake()
    {
        handSign = transform.parent.gameObject.GetComponent<HandSign>();
        render   = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        render.positionCount = 0;
        render.enabled = false;
    }

    void Update()
    {
        if (handSign == null) { return; }
        if (handSign.isRF == null) { return; }
        if (handSign.isRF._skeleton.Bones.Count <= 0) { return; }

        //if (handSign.deuces || grab.isIndexPinching)
        if (handSign.deuces)
        {
            render.enabled = true;

            int NextPositionIndex = render.positionCount;
            render.positionCount = NextPositionIndex + 1;
            render.SetPosition(NextPositionIndex, handSign.isRF._skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position);
        }
        else
        {
            render.enabled = false;
            
            // 描画終了時
            if (oldRenderEnabled && !render.enabled)
            {
                GetComponent<LineToObject>().LineCorrection();
            }

            // 初期化
            render.positionCount = 0;
            transform.position = handSign.isRF._skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        }

        oldRenderEnabled = render.enabled;
    }
}
