using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPadOpen : MonoBehaviour
{
    GameObject eventPadObj;
    bool buttonFlg = false;
    float speed = 0.005f;
    float progress = 0.0f;
    bool endFlg = true;
    Vector3 startPos;
    Vector3 goalPos;

    public bool GetEndFlg() { return endFlg; }
    public bool GetButtonFlg() { return buttonFlg; }

    //カメラを切り替えるためにメインのカメラとサブのカメラ
    private GameObject mainCamera;
    private GameObject subCamera;

    //文字を消しておくためのオブジェクト
    private GameObject canvas;

    void SetPadOpenPos()
    {
        if (eventPadObj == GetComponent<GetClickedGameObject>().GetGameObject())
        {
            Debug.Log("クリック");

            buttonFlg = true;

            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            startPos = pos;

            goalPos = pos;

            goalPos.x -= 30;

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

        startPos.x += 30;

        ScreenErase();
    }

    void PadOpen()
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
            ScreenStartUP();
        }
        else
        {
            endFlg = false;
        }

        myTransform.position = pos;  // 座標を設定
    }

    void PadClose()
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

    //修理画面を消す処理(スタート時)
    void ScreenEraseStart()
    {
        canvas.SetActive(false);
    }

    //修理画面を消す処理(スタート時以外)
    void ScreenErase()
    {
        canvas.SetActive(false);
        mainCamera.SetActive(true);
        subCamera.SetActive(false);
    }

    //修理画面を付ける処理
    void ScreenStartUP()
    {
        canvas.SetActive(true);
        mainCamera.SetActive(false);
        subCamera.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        eventPadObj = GameObject.Find("EventPadObj");

        canvas = GameObject.Find("EventCanvas");
        mainCamera = GameObject.Find("Main Camera");
        subCamera = GameObject.Find("CRT Camera");

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;

        startPos = pos;

        goalPos = pos;

        goalPos.x -= 30;

        ScreenEraseStart();
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
            PadOpen();
        }
        else
        {
            PadClose();
        }
    }
}
