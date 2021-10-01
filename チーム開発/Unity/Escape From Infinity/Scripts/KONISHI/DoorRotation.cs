using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    public GameObject Door;
    public GameObject Left;
    public GameObject Right;

    public DoubleSwitch DS1;
    public DoubleSwitch DS2;

    public CameraManager cameraManager;

    //ドアが開いたか
    private bool doorOpenFlg = false;

    //総回転量
    private float totalRot = 0;

    private bool b_Door;

    // Start is called before the first frame update
    void Start()
    {
        Door = GameObject.Find("Door");
        Left = GameObject.Find("Cube.001");
        Right = GameObject.Find("Cube.002");

        b_Door = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(DS1.push && DS2.push)
        {
            if (!doorOpenFlg)
            {
                if (!b_Door)
                {
                    cameraManager.SetCamera = true;
                    b_Door = true;
                }

                //扉を開く
                float rot = 1.2f;
                Left.transform.Rotate(new Vector3(0, 0, -rot));
                Right.transform.Rotate(new Vector3(0, 0, rot));
                totalRot += rot;
                if (totalRot >= 90) { doorOpenFlg = true; }
            }
        }
        else
        {
            if(doorOpenFlg)
            {
                //扉を閉じる
                float rot = 1.2f;
                Left.transform.Rotate(new Vector3(0, 0, rot));
                Right.transform.Rotate(new Vector3(0, 0, -rot));
                totalRot -= rot;
                if (totalRot <= 0) { doorOpenFlg = false; }
            }
            //中途半端な開き方の時
            else if(totalRot >= 0)
            {
                //扉を閉じる
                float rot = 0.9f;
                Left.transform.Rotate(new Vector3(0, 0, rot));
                Right.transform.Rotate(new Vector3(0, 0, -rot));
                totalRot -= rot;
            }
        }
    }
}
