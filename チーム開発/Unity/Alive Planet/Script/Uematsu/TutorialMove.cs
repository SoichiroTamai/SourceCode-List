using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : MonoBehaviour
{
    //チュートリアルの画像
    private GameObject TutorialImage;
    //画像と画像の距離
    private const float DistanceImage = 720;

    //画像が動いているのかまた、どちら側に動いているのか
    private bool MoveFlg = false;

    //移動のゴールとなる場所の座標
    private Vector3 StartPos;
    private Vector3 GorlPos;

    //二点間の距離を入れる
    private float distance_two;

    //移動スピード
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        TutorialImage = GameObject.Find("TutorialImage_1");
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveFlg)
        {
            //現在の位置
            float present_Location = (Time.time * speed) / distance_two;

            //オブジェクトの位置
            TutorialImage.transform.position = Vector3.Lerp(StartPos, GorlPos, present_Location);

            //ゴールまで到着していたら処理をやめる
            if(present_Location>=1.0f)
            {
                MoveFlg = false;
            }
        }
    }

    //Nextボタンを押されたら
    public void OnButtonNext()
    {
        //スタートの位置とゴールの位置を設定する
        StartPos = TutorialImage.transform.position;
        GorlPos = StartPos;
        GorlPos.x -= DistanceImage;

        //フラグを立てて処理をスタート
        MoveFlg = true;
    }

    //Backボタンを押されたら
    public void OnButtonBack()
    {
        //スタートの位置とゴールの位置を設定する
        StartPos = TutorialImage.transform.position;
        GorlPos = StartPos;
        GorlPos.x += DistanceImage;

        //フラグを立てて処理をスタート
        MoveFlg = true;
    }
}
