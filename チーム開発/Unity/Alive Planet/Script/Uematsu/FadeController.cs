using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public float FadeSpeed = 0.02f;//透明度が変わるスピードを管理
    float alpha = 1;                      //パネルの不透明度を管理

    private bool isFadeOut = false;       //フェードアウト処理の開始、完了を管理するフラグ
    private bool isFadeIn = false;        //フェードイン処理の開始、完了を管理するフラグ

    Image fadeImage;                      //透明度を管理する

    public string NextScene = "";

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        alpha = fadeImage.color.a;
        StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadeOut)
        {
            FadeOut();
        }

        if(isFadeIn)
        {
            FadeIn();
        }
    }

    void FadeOut()
    {
        alpha += FadeSpeed;//不透明度を徐々に上げる
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, alpha);//変更した不透明度をパネルに反映する

        //1以上になったら処理終了
        if(alpha>=1)
        {
            SceneManager.LoadScene(NextScene);
            isFadeOut = false;
        }
    }

    public void StartFadeOut()
    {
        isFadeOut = true;        //フェードアウトを開始する
        fadeImage.enabled = true;//パネルの表示をONに
    }

    void FadeIn()
    {
        alpha -= FadeSpeed;//不透明度を徐々に上げる
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, alpha);//変更した不透明度をパネルに反映する

        //0まで下がったら処理終了
        if (alpha <= 0)
        {
            isFadeIn = false;
            fadeImage.enabled = false;
        }
    }

    public void StartFadeIn()
    {
        isFadeIn = true;        //フェードアウトを開始する
        fadeImage.enabled = true;//パネルの表示をONに
    }

    public void SetAlpha(int alpha)
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, alpha);
    }
}
