using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera: MonoBehaviour
{
    private GameObject player;       //プレイヤー格納用

    // Use this for initialization
    void Start()
    {
        //unitychanをplayerに格納
        player = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        //左シフトが押されている時
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //ユニティちゃんを中心に-5f度回転
            transform.RotateAround(player.transform.position, Vector3.up, -5f);
        }
        //右シフトが押されている時
        else if (Input.GetKey(KeyCode.RightShift))
        {
            //ユニティちゃんを中心に5f度回転
            transform.RotateAround(player.transform.position, Vector3.up, 5f);
        }
    }
}
