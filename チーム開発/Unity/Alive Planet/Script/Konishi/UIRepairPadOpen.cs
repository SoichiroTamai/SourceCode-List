using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRepairPadOpen : MonoBehaviour
{
    GameObject repairPadObj;

    bool buttonFlg = false;
    float speed = 0.005f;
    float progress = 0.0f;
    bool endFlg = true;
    float move = 1200;
    Vector3 startPos;
    Vector3 goalPos;

    void SetPadOpenPos()
    {
        if (repairPadObj == GetComponent<GetClickedGameObject>().GetGameObject())
        {
            buttonFlg = true;

            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            startPos = pos;

            goalPos = pos;

            goalPos.x -= move;

            GetComponent<GetClickedGameObject>().DeleteGameObject();
        }
    }

    public void SetPadClosePos()
    {
        buttonFlg = false;

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;

        startPos = pos;

        goalPos = pos;

        startPos.x += move;
    }

    // Start is called before the first frame update
    void Start()
    {
        repairPadObj = GameObject.Find("RepairPadObj");

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;

        startPos = pos;

        goalPos = pos;

        goalPos.x -= move;
    }

    // Update is called once per frame
    void Update()
    {
        if (endFlg)
        {
            if (!buttonFlg)
            {
                SetPadOpenPos();
            }
        }

        if (buttonFlg)
        {
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            Vector3 vStart = startPos;
            Vector3 vGoal = goalPos;

            //目標地点までのベクトル
            Vector3 vTo = vGoal - vStart;

            //進行具合を加味して現在の地点を割り出す
            Vector3 vNow = vStart + vTo * Mathf.Sin((progress * Mathf.PI) / 2);

            //地点を中間に合わせる
            pos = vNow;

            //進行具合の更新
            progress += speed;
            if (progress >= 1.0f)
            {
                progress = 1.0f;
                endFlg = true;
            }
            else
            {
                endFlg = false;
            }

            myTransform.position = pos;  // 座標を設定
        }
        else
        {
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            Vector3 vStart = startPos;
            Vector3 vGoal = goalPos;

            //目標地点までのベクトル
            Vector3 vTo = vGoal - vStart;

            //進行具合を加味して現在の地点を割り出す
            Vector3 vNow = vStart + vTo * Mathf.Sin((progress * Mathf.PI) / 2);

            //地点を中間に合わせる
            pos = vNow;

            //進行具合の更新
            progress -= speed;
            if (progress <= 0.0f)
            {
                progress = 0.0f;
                endFlg = true;
            }
            else
            {
                endFlg = false;
            }

            myTransform.position = pos;  // 座標を設定
        }
    }
}
