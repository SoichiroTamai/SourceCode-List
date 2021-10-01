using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject Player_Base;　//Player_Baseを入れる変数
    private Player Player_Flag;　//Player_Flagを参照する変数
    private bool is_Grab;
    private bool is_Grabbing;
    private bool is_Release;

    // Start is called before the first frame update
    void Start()
    {
        //Player_Baseの情報を取得
        Player_Base = GameObject.Find("Player");
        //Player_BaseのPlayer_Flagを取得
        Player_Flag = Player_Base.GetComponent<Player>();

        is_Release = true;
    }

    // Update is called once per frame
    void Update()
    {
        is_Grab = Player_Flag.is_Grab;

        if (is_Grab)
        {
            Transform playerTransform = Player_Flag.transform;

            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = playerTransform.position;

            pos.y = 2;

            myTransform.position = pos;

            is_Release = false;
        }
        else
        {
            if (!is_Release)
            {
                Transform playerTransform = Player_Flag.transform;

                // transformを取得
                Transform myTransform = this.transform;

                // 座標を取得
                Vector3 pos = playerTransform.position;

                pos.z = 1;

                myTransform.position = pos;

                is_Release = true;
            }
        }
    }
}
