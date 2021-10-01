using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorl : MonoBehaviour
{
    //クリアパネルを追加する
    public GameObject ClearCanvas;
    //スコアを管理するオブジェクトを追加
    public GameObject ScoreManager;

    private void Start()
    {
        ScoreManager = GameObject.Find("ScoreManeger");
    }

    //プレイヤーが当たり判定に入った時の処理
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            //ゲームクリアのキャンバスを表示
            ClearCanvas.SetActive(true);
            //経過時間を計測するタイマーを止める
            ScoreManager.GetComponent<ScoreManeger>().StopTime(true);
            //時間と挑戦回数を管理するScript内から経過時間と挑戦回数を取得する
            ScoreManager.GetComponent<ScoreManeger>().DisplayScore();
            //マウスカーソルを出現させる
            Cursor.visible = true;
            //マウスカーソルを自由に動かせるようにする
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
