using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeSprite : MonoBehaviour
{
    // SpriteRenderer コンポーネントを格納する変数
    private SpriteRenderer m_spriteRenderer;
    // Image コンポーネントを格納する変数
    private Image m_image;
    // スプライトオブジェクトを格納する配列
    public Sprite[] m_Sprite;
    // スプライトオブジェクトを変更するためのフラグ
    bool Change;
    // 時間計測用変数
    float time;
    // 終了時間変数
    public float endTime;
    // UIか
    public bool isUI = false;

    // ゲーム開始時に実行する処理
    void Start()
    {
        // スプライトオブジェクトを変更するためのフラグを false に設定
        Change = false;

        // UIにはSpriteRendererが無いので、UIかどうかを判定
        if(isUI)
        {
            // Image コンポーネントを取得して変数 m_image に格納
            m_image = GetComponent<Image>();
        }
        else
        {
            // SpriteRenderer コンポーネントを取得して変数 m_spriteRenderer に格納
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // ゲーム実行中に毎フレーム実行する処理
    void Update()
    {
        // 時間経過
        time += Time.deltaTime;

        // 終了時間まで経ったら
        if (time > endTime)
        {
            // 時間リセット
            time = 0;

            // スプライトオブジェクトの変更フラグが true の場合
            if (Change)
            {
                // スプライトオブジェクトの変更
                //（配列 m_Sprite[0] に格納したスプライトオブジェクトを変数 m_spriteRenderer に格納したSpriteRenderer コンポーネントに割り当て）
                if (isUI) { m_image.sprite = m_Sprite[0]; }
                else { m_spriteRenderer.sprite = m_Sprite[0]; }

                Change = false;
            }
            // スプライトオブジェクトの変更フラグが false の場合
            else
            {
                // スプライトオブジェクトの変更
                //（配列 m_Sprite[1] に格納したスプライトオブジェクトを変数 m_spriteRenderer に格納したSpriteRenderer コンポーネントに割り当て）
                if (isUI) { m_image.sprite = m_Sprite[1]; }
                else { m_spriteRenderer.sprite = m_Sprite[1]; }
                Change = true;
            }
        }
    }
}
