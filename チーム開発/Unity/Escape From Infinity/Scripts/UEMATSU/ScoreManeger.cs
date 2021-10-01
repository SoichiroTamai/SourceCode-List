using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour
{
    //スコア(数値)
    //private int Sucore;

    // 経過時間
    public  Text  TimeText    = null;   // クリアまでにかかった時間を表示するテキスト
    public  Text  NowTimeText = null;   // 現在の経過時間
    private float Seconds;              // 秒
    private float OldSeconds;           // 1フレーム前の秒数
    private bool  FreezTime   = false;  // 時間のカウントを止めるための変数

    // メビウスシステムの使用回数
    public  Text TryText    = null;     // メビウスシステムを使った回数を表示するテキスト
    public  Text NowTryText = null;     // メビウスシステムを使った回数を表示するテキスト
    private int  TryNum     = 0;        // 使用した回数

    // Start is called before the first frame update
    void Start()
    {
        // 現在の状況を表示するテキストを取得
        NowTimeText = GameObject.Find("Elapsed Time").GetComponent<Text>();
        NowTryText = GameObject.Find("MöbiusSystem Counter").GetComponent<Text>();

        if (!NowTimeText) { Debug.Log("ScoreManeger：NowTimeText 取得失敗"); }
        else { NowTimeText.text = "Time" + "\u00A0" + "00:00:00"; }                 // ※ \u00A0 … ノーブレイクスペース(自動改行されない空白)

        if (!NowTryText) { Debug.Log("ScoreManeger：NowTryText 取得失敗"); }
        else { NowTryText.text = "Try" + "\u00A0" + "0"; }

        // スコアリセット
        //Sucore = 0;

        // 時間停止用フラグのリセット
        FreezTime = false;

        //時間と挑戦回数のリセット
        Seconds    = 0f;
        OldSeconds = 0f;
        TryNum     = 0;
        FreezTime  = false;
    }

    // Update is called once per frame
    void Update()
    {
        //時間を進めないときはここで帰る
        if (FreezTime) { return; }

        //時間を進める
        Seconds += Time.deltaTime;

        // 1秒毎に現在の経過時間の表示を更新
        if ((int)Seconds != (int)OldSeconds)
        {
            NowTimeText.text = "Time\u00A0" + GetTime().x.ToString("00") + ":" + GetTime().y.ToString("00") + ":" + GetTime().z.ToString("00");
        }

        // 1フレーム前の時間を記憶しておく
        OldSeconds = Seconds;
    }

    public void StopTime(bool isStop)
    {
        // 時間のカウントを止める
        FreezTime = isStop;
    }

    // メビウスシステムの回数を一回増やす
    public void AddTryNum()
    {
        // 挑戦回数を一回増やす
        TryNum += 1;

        // 表示テキストの更新
        NowTryText.text = "Try" + "\u00A0" + TryNum.ToString("0");
    }

    public void DisplayScore()
    {
        //時間と挑戦の回数をUIのテキストに表示する
        TimeText.text = GetTime().x.ToString("00") + ":" + GetTime().y.ToString("00") + ":" + GetTime().z.ToString("00");
        TryText.text = TryNum.ToString("N0") + "Num";
    }

    public float GetSeconds()
    {
        return Seconds;
    }

    public int GetNum()
    {
        return TryNum;
    }

    // 経過時間を 時分秒 (h,m,s) で取得
    public Vector3 GetTime()
    {
        Vector3 time;

        time.x = Seconds / 3600;        // 時(h)
        time.y = Seconds % 3600 / 60;   // 分(m)
        time.z = Seconds % 60;          // 秒(s)

        time.x = (int)time.x;
        time.y = (int)time.y;
        time.z = (int)time.z;

        return time;
    }
}