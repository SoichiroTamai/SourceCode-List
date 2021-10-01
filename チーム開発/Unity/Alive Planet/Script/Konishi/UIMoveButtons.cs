using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoveButtons : MonoBehaviour
{
    // カメラ
    public Camera cam;

    // カメラの座標
    Transform Pos;

    // 移動速度
    float MoveSpeed = 0.05f;

    // 押されているか
    bool UpButtonPush = false;
    bool DownButtonPush = false;
    bool LeftButtonPush = false;
    bool RightButtonPush = false;

    private void Start()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }

        // カメラオブジェクトの座標を取得
        Pos = cam.GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    // それぞれのボタンが押されている状態かを取得する
    public void UpButtonPushDown()
    {
        UpButtonPush = true; 
    }

    public void UpButtonPushUp()
    {
        UpButtonPush = false;
    }

    public void DownButtonPushDown()
    {
        DownButtonPush = true;
    }

    public void DownButtonPushUp()
    {
        DownButtonPush = false;
    }

    public void LeftButtonPushDown()
    {
        LeftButtonPush = true;
    }

    public void LeftButtonPushUp()
    {
        LeftButtonPush = false;
    }

    public void RightPushDown()
    {
        RightButtonPush = true;
    }

    public void RightPushUp()
    {
        RightButtonPush = false;
    }

    // 移動処理
    public void MoveUp()
    {
        Pos.position = Pos.position + new Vector3(0.0f, MoveSpeed, 0.0f);
    }

    public void MoveDown()
    {
        Pos.position = Pos.position - new Vector3(0.0f, MoveSpeed, 0.0f);
    }

    public void MoveLeft()
    {
        Pos.position = Pos.position - new Vector3(MoveSpeed, 0.0f, 0.0f);
    }

    public void MoveRight()
    {
        Pos.position = Pos.position + new Vector3(MoveSpeed, 0.0f, 0.0f);
    }

    // カメラを移動
    private void Move()
    {
        // キーボード入力
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // UIボタン入力
        if (UpButtonPush)
        {
            MoveUp();
        }
        else if (DownButtonPush)
        {
            MoveDown();
        }
        else if (LeftButtonPush)
        {
            MoveLeft();
        }
        else if(RightButtonPush)
        {
            MoveRight();
        }
    }
}
