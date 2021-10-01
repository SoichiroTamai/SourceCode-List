using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public float FadeSpeed = 0.02f;//�����x���ς��X�s�[�h���Ǘ�
    float alpha = 1;                      //�p�l���̕s�����x���Ǘ�

    private bool isFadeOut = false;       //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    private bool isFadeIn = false;        //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O

    Image fadeImage;                      //�����x���Ǘ�����

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
        alpha += FadeSpeed;//�s�����x�����X�ɏグ��
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, alpha);//�ύX�����s�����x���p�l���ɔ��f����

        //1�ȏ�ɂȂ����珈���I��
        if(alpha>=1)
        {
            SceneManager.LoadScene(NextScene);
            isFadeOut = false;
        }
    }

    public void StartFadeOut()
    {
        isFadeOut = true;        //�t�F�[�h�A�E�g���J�n����
        fadeImage.enabled = true;//�p�l���̕\����ON��
    }

    void FadeIn()
    {
        alpha -= FadeSpeed;//�s�����x�����X�ɏグ��
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, alpha);//�ύX�����s�����x���p�l���ɔ��f����

        //0�܂ŉ��������珈���I��
        if (alpha <= 0)
        {
            isFadeIn = false;
            fadeImage.enabled = false;
        }
    }

    public void StartFadeIn()
    {
        isFadeIn = true;        //�t�F�[�h�A�E�g���J�n����
        fadeImage.enabled = true;//�p�l���̕\����ON��
    }

    public void SetAlpha(int alpha)
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g,
            fadeImage.color.b, alpha);
    }
}
