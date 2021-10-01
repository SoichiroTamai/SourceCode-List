using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characterfeed : MonoBehaviour
{
    public string[] sentences;//文字を格納する
    public Sprite[] BackTexture;//イメージを格納する
    public string[] spriteName;//イメージの名前を適応する
    public SpriteRenderer uiBackTexture;//背景のテクスチャの参照
    public SpriteRenderer uiFrontTexture;//正面のテクスチャの参照
    [SerializeField] Text uitext;//uitextへの参照

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;//1文字の表示にかける時間

    private int currentSentenceNum = 0;//現在表示している文章番号
    private string currentSentence = string.Empty;//現在の文字列
    private float timeUntilDisplay = 0;//表示にかかる時間
    private float timeBeganDisplay = 1;//文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;//現在表示中の文字数

    //画面外の動かす始めの位置
    private Vector3 NewPos = new Vector3(500, 0);
    //画面移動のスタートの場所と終わりの場所
    private Vector3 startPos;
    private Vector3 gorlPos;
    float progress = 0.0f;

    //画面のフェードインアウトの管理
    public FadeController fadeController;

    //燃料をセット
    public GameMasterScript masterScript;
    //初期燃料
    public int startFuel = 30;

    // Start is called before the first frame update
    void Start()
    {
        SetNextSentence();
        masterScript.FuelVault += startFuel;
    }

    // Update is called once per frame
    void Update()
    {
        //文字の表示完了/未完了
        if(IsDisplayComplete())
        {
            
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
            if(Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }

            //正面の画像を移動させる
            uiFrontTexture.transform.position = LerpMoveImage(uiFrontTexture.transform.position, NewPos,
                uiBackTexture.transform.position);
        }

        //今表示される文字数を計算
        int DisplayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //さっき計算した文字数が現在表示している文字数と違えば
        if(DisplayCharCount!=lastUpdateCharCount)
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
        //スクロールのスタート位置を変更する
        uiFrontTexture.transform.position = NewPos;
        Debug.Log(uiFrontTexture.transform.position);
        //変数の型を変える
        uiBackTexture.GetComponent<SpriteRenderer>();
        uiFrontTexture.gameObject.GetComponent<SpriteRenderer>();
        //格納されている画像を変更する
        uiBackTexture.sprite = BackTexture[currentSentenceNum];
        uiFrontTexture.sprite = BackTexture[currentSentenceNum + 1];
        
        //現在の経過時間を開始時間として保持している
        timeBeganDisplay = Time.time;
        currentSentenceNum += 1;

        Debug.Log(currentSentence.Length);

        //0番目を表示できるようにする
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        //終了するかを時間で確認している
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }

    Vector3 LerpMoveImage(Vector3 pos,Vector3 start,Vector3 end)
    {
        //開始地点と終了地点を設定
        Vector3 vStartPos = start;
        Vector3 vEndPos = end;

        //目的地までのベクトル
        Vector3 vTo = vEndPos - vStartPos;

        //進行具合を加味して現在の地点を割り出す
        Vector3 vNow = vStartPos + vTo * Mathf.Sin((progress * Mathf.PI) / 2);

        //地点を中間に合わせる
        pos = vNow;

        //進行情報の更新
        progress = Mathf.Clamp01((Time.time / (timeBeganDisplay + timeUntilDisplay)));

        //Debug.Log(pos);

        return pos;
    }
}
