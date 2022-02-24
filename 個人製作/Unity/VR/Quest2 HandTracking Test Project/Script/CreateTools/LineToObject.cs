using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToObject : MonoBehaviour
{
    struct lineCorrection
    {
        public Vector3 pos;
        public bool isCorrection;   // 補正の有無 (true = 補正あり) ※ 読み飛ばす
        public bool isCorner;       // 角かどうか
    }

    private lineCorrection[] lineCorrections;
    private DrawLine drawLine;
    private float nearPointDetectValue;

    // 確認用
    List<GameObject> _spheres = new List<GameObject>();

    private void Awake()
    {
        drawLine = GetComponent<DrawLine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nearPointDetectValue = 0.01f * 0.01f; //1cm以下は手の震えとみなす

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ライン補正
    public void LineCorrection()
    {
        if (drawLine.GetRenderer().positionCount <= 0) { return; }

        lineCorrections = new lineCorrection[drawLine.GetRenderer().positionCount];

        lineCorrections[0].pos = drawLine.GetRenderer().GetPosition(0);
        lineCorrections[0].isCorrection = false;

        for (int i = 1; i < lineCorrections.Length; i++)
        {
            lineCorrections[i].pos = drawLine.GetRenderer().GetPosition(i);

            var currentVec = (lineCorrections[i].pos - lineCorrections[i-1].pos);

            // 移動量が少ない場合はラインの頂点とみなさない
            if(currentVec.sqrMagnitude <= nearPointDetectValue)
            {
                lineCorrections[i].isCorrection = true;
            }
            else
            {
                // 確認用
                //CorrecPosCreateObj(i);

                // 角判定

            }
        }

        Debug.Log("LineCorrection");
    }

    // 頂点確認用 オブジェクト生成
    private void CorrecPosCreateObj(int num)
    {
        //  Sphereを生成しボーンに割り当てる
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = lineCorrections[num].pos;
        sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        Color color = new Color(0.0f, 0.0f, 1.0f, 1.0f);

        sphere.GetComponent<Renderer>().material.color = color;

        _spheres.Add(sphere);
    }

    //動かない
    //private void CorrecLineDraw()
    //{
    //    LineRenderer render = drawLine.GetRenderer();

    //    for (int i = 0; i < lineCorrections.Length-1; i++)
    //    {
    //        int NextPositionIndex = render.positionCount;
    //        render.positionCount = NextPositionIndex + 1;
    //        render.SetPosition(i, lineCorrections[i].pos);
    //    }
    //}
}