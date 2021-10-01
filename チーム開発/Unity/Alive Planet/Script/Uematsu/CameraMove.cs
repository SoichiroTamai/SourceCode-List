using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //カメラの座標
    Transform Pos;
    //メインカメラ
    Camera Cam;
    //ゲームマネージャー
    public GameMasterScript GameMaster;

    //動くスピード
    public float MoveSpeed = 0.05f;
    //ズームのキーコード
    public KeyCode ZoomIn = KeyCode.I;
    public KeyCode ZoomOut = KeyCode.O;

    // Start is called before the first frame update
    private void Start()
    {
        //アタッチしたオブジェクトの座標を取得
        Pos = this.gameObject.GetComponent<Transform>();
        //アタッチしたオブジェクトのカメラを取得
        Cam = this.gameObject.GetComponent<Camera>();

        //カメラの位置をプレイヤーに合わせる
        Pos.position = Pos.position + new Vector3(GameMaster.PlayerPos.x, GameMaster.PlayerPos.y, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        Zoom();
    }

    //カメラを上下左右キーで移動
    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Pos.position = Pos.position + new Vector3(0.0f, MoveSpeed, 0.0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Pos.position = Pos.position - new Vector3(0.0f, MoveSpeed, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Pos.position = Pos.position - new Vector3(MoveSpeed, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Pos.position = Pos.position + new Vector3(MoveSpeed, 0.0f, 0.0f);
        }
    }

    //指定したキーを押すとズームインとズームアウトをする
    private void Zoom()
    {
        if(Input.GetKey(ZoomIn))
        {
            Cam.orthographicSize = Cam.orthographicSize - MoveSpeed;
        }

        if (Input.GetKey(ZoomOut))
        {
            Cam.orthographicSize = Cam.orthographicSize + MoveSpeed;
        }
    }
}
