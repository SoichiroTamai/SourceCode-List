using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    //ハイスコアを表示するパネル
    public GameObject RecordPanel;
    //ハイスコア管理するオブジェクト
    public Recordholder RecordHolder;
    //今のスコアを管理するオブジェクト
    public GameObject ScorePanel;

    public void OnClickStartButton()
    {
        //スコアが前の物より良ければ変更する
        RecordHolder.ChangeHigeScore();
        //ハイスコアを表示する処理
        RecordHolder.DisplayHigeScore();
        //ハイスコアのパネルを表示する
        RecordPanel.SetActive(true);
        //前のスコアのパネルを消す
        ScorePanel.SetActive(false);
    }
}
