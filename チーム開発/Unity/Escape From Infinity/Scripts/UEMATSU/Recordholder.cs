using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recordholder : MonoBehaviour
{
    //今の時間を取得するためのオブジェクト
    public GameObject ScoreManaget = null;

    //時間
    public Text HighTimeText = null;    // 今までで一番速かったタイムを表示するテキスト
    private float ClearTime;            // 現在のゲームのスコアタイム
    private float HighSeconds = 1000;      // ハイスコアの時間

    //使用回数
    public Text HighTryText = null;     // 一番メビウスシステムを使った回数が少ない時の回数を表示するテキスト
    private int ClearNum;               // 現在のゲームの使用回数
    private int HighTryNum = 1000;         // 一番少ない使用した回数

    // Start is called before the first frame update
    void Start()
    {
        //現在のスコアを管理しているスクリプトを作成
        ScoreManaget = GameObject.Find("ScoreManeger");

        if (!ScoreManaget) { Debug.Log("Record holder;ScoreManager 取得失敗"); }

        //ハイスコアを取得するテキストを作成
        //HighTimeText = GameObject.Find("HighTime").GetComponent<Text>();
        //HighTryText = GameObject.Find("HighTryNum").GetComponent<Text>();

        //if (!HighTimeText) { Debug.Log("Record holder:HighTimeText 取得失敗"); }
        //else { HighTimeText.text = "High Score Time" + "\u00A0" + "00:00:00"; }//\u00A0 … ノーブレイクスペース(自動改行されない空白)

        //if (!HighTryText) { Debug.Log("Record holder：HighTryText 取得失敗"); }
        //else { HighTryText.text = "High Score Try" + "\u00A0" + "0"; }

        //ハイスコアをリセットしてからデバッグする(ゲームにする際はここは削除)
        PlayerPrefs.SetFloat("SECONDS", 1000);
        PlayerPrefs.SetInt("TRYNUM", 1000);

        //ハイスコアのロード
        HighSeconds = PlayerPrefs.GetFloat("SECONDS", 1000);
        HighTryNum = PlayerPrefs.GetInt("TRYNUM", 1000);
    }

    private void OnDestroy()
    {
        //スコアを保存
        PlayerPrefs.SetFloat("SECONDS", HighSeconds);
        PlayerPrefs.SetInt("TRYNUM", HighTryNum);

        PlayerPrefs.Save();
    }


    public void DisplayHigeScore()
    {
        HighTimeText.text = GetTime().x.ToString("00") + ":" + GetTime().y.ToString("00") + ":" + GetTime().z.ToString("00");
        HighTryText.text = HighTryNum.ToString("00") + "Num";
    }

    public void ChangeHigeScore()
    {
        ClearTime = ScoreManaget.GetComponent<ScoreManeger>().GetSeconds();
        ClearNum = ScoreManaget.GetComponent<ScoreManeger>().GetNum();

        if(HighSeconds>ClearTime)
        {
            HighSeconds = ClearTime;
        }

        if(HighTryNum>ClearNum)
        {
            HighTryNum = ClearNum;
        }
    }

    public Vector3 GetTime()
    {
        Vector3 time;

        time.x = HighSeconds / 3600;        // 時(h)
        time.y = HighSeconds % 3600 / 60;   // 分(m)
        time.z = HighSeconds % 60;          // 秒(s)

        time.x = (int)time.x;
        time.y = (int)time.y;
        time.z = (int)time.z;

        return time;
    }
}
