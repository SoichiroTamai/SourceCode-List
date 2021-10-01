using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characterfeed_text : MonoBehaviour
{
    public string[] sentences;//文字を格納する
    [SerializeField] Text uitext;//uitextへの参照
    public KeyCode NextkeyCode;//次へ行くためのボタン

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;//1文字の表示にかける時間

    private int currentSentenceNum = 0;//現在表示している文章番号
    private string currentSentence = string.Empty;//現在の文字列
    private float timeUntilDisplay = 0;//表示にかかる時間
    private float timeBeganDisplay = 1;//文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;//現在表示中の文字数

    //画面のフェードインアウトの管理
    public FadeController fadeController;

    // Start is called before the first frame update
    void Start()
    {
        SetNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        //文字の表示完了/未完了
        if (IsDisplayComplete())
        {
            //最後の文字ではないなおかつボタンが押された
            if (Input.GetMouseButtonDown(0))
            {
                //最後の文章ではないなおかつボタンが押された
                if (currentSentenceNum < sentences.Length)
                {
                    SetNextSentence();
                }
                //最後の文章でなおかつボタンが押された
                else
                {
                    fadeController.StartFadeOut();
                }
            }
        }
        else
        {
            //ボタンが押された
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }

        //今表示される文字数を計算
        int DisplayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //さっき計算した文字数が現在表示している文字数と違えば
        if (DisplayCharCount != lastUpdateCharCount)
        {
            uitext.text = currentSentence.Substring(0, DisplayCharCount);
            //表示している文字数の更新
            lastUpdateCharCount = DisplayCharCount;
        }
    }

    void SetNextSentence()
    {
        //現在表示している文字列に全体の中の何番目みたいな感じで格納する
        currentSentence = sentences[currentSentenceNum];
        //表示時間を文字列の長さ×表示時間みたいにして設定する
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        
        //現在の経過時間を開始時間として保持している
        timeBeganDisplay = Time.time;
        currentSentenceNum += 1;

        //0番目を表示できるようにする
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        //終了するかを時間で確認している
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }
}
