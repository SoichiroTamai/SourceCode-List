using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.z += 0.005f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.z -= 0.005f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 0.005f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += 0.005f;
        }

        myTransform.position = pos;  // 座標を設定
    }
}
