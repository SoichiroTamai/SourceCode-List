using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeComera : MonoBehaviour
{
    //カメラのオブジェクト
    public GameObject ScanlineCam;
    public GameObject CRTCam;
    public GameObject VHSCam;

    // Start is called before the first frame update
    void Start()
    {
        //カメラのオブジェクトをセットする
        ScanlineCam = GameObject.Find("Main Camera");
        CRTCam = GameObject.Find("CRTCam");
        VHSCam = GameObject.Find("VHSCam");

        //ランダムを使用し
        float rand = Random.Range(0.0f, 3.0f);

        //どのカメラを使うか決める
        if(rand>=0.0f&&rand<=1.0f)
        {
            ScanlineCam.SetActive(true);
            CRTCam.SetActive(false);
            VHSCam.SetActive(false);
        }
        else if(rand>1.0f&&rand<=2.0f)
        {
            ScanlineCam.SetActive(false);
            CRTCam.SetActive(true);
            VHSCam.SetActive(false);
        }
        else if(rand>2.0f)
        {
            ScanlineCam.SetActive(false);
            CRTCam.SetActive(false);
            VHSCam.SetActive(true);
        }

        Debug.Log(rand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
