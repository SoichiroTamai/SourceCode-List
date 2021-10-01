using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public GameObject score_object = null;
    public GameObject ScoreManaget = null;

    private float RankTime;
    private float RankNum;
    private float Time;
    private float Num;
    private string rank;
    // Start is called before the first frame update
    void Start()
    {
        RankTime = 60;
        RankNum = 3;
        ScoreManaget = GameObject.Find("ScoreManeger");
    }

    // Update is called once per frame
    void Update()
    {
        Time = ScoreManaget.GetComponent<ScoreManeger>().GetSeconds();
        Num  = ScoreManaget.GetComponent<ScoreManeger>().GetNum();

        // オブジェクトからTextコンポーネントを取得
        Text score_text = score_object.GetComponent<Text>();

        if(Time<RankTime&& Num<RankNum)
        {
            rank = "S";
        }
        else if (Time < RankTime || Num < RankNum)
        {
            rank = "A";
        }
        else if (Time > RankTime && Num > RankNum)
        {
            rank = "B";
            if (Time > (RankTime * 1.5) || Num > (RankNum + 2))
            {
                rank = "C";
            }
        }

        // テキストの表示を入れ替える
        score_text.text = rank;
    }
}
