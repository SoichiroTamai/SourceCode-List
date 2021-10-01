using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characterfeed_text : MonoBehaviour
{
    public string[] sentences;//�������i�[����
    [SerializeField] Text uitext;//uitext�ւ̎Q��
    public KeyCode NextkeyCode;//���֍s�����߂̃{�^��

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;//1�����̕\���ɂ����鎞��

    private int currentSentenceNum = 0;//���ݕ\�����Ă��镶�͔ԍ�
    private string currentSentence = string.Empty;//���݂̕�����
    private float timeUntilDisplay = 0;//�\���ɂ����鎞��
    private float timeBeganDisplay = 1;//������̕\�����J�n��������
    private int lastUpdateCharCount = -1;//���ݕ\�����̕�����

    //��ʂ̃t�F�[�h�C���A�E�g�̊Ǘ�
    public FadeController fadeController;

    // Start is called before the first frame update
    void Start()
    {
        SetNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        //�����̕\������/������
        if (IsDisplayComplete())
        {
            //�Ō�̕����ł͂Ȃ��Ȃ����{�^���������ꂽ
            if (Input.GetMouseButtonDown(0))
            {
                //�Ō�̕��͂ł͂Ȃ��Ȃ����{�^���������ꂽ
                if (currentSentenceNum < sentences.Length)
                {
                    SetNextSentence();
                }
                //�Ō�̕��͂łȂ����{�^���������ꂽ
                else
                {
                    fadeController.StartFadeOut();
                }
            }
        }
        else
        {
            //�{�^���������ꂽ
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }

        //���\������镶�������v�Z
        int DisplayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //�������v�Z���������������ݕ\�����Ă��镶�����ƈႦ��
        if (DisplayCharCount != lastUpdateCharCount)
        {
            uitext.text = currentSentence.Substring(0, DisplayCharCount);
            //�\�����Ă��镶�����̍X�V
            lastUpdateCharCount = DisplayCharCount;
        }
    }

    void SetNextSentence()
    {
        //���ݕ\�����Ă��镶����ɑS�̂̒��̉��Ԗڂ݂����Ȋ����Ŋi�[����
        currentSentence = sentences[currentSentenceNum];
        //�\�����Ԃ𕶎���̒����~�\�����Ԃ݂����ɂ��Đݒ肷��
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        
        //���݂̌o�ߎ��Ԃ��J�n���ԂƂ��ĕێ����Ă���
        timeBeganDisplay = Time.time;
        currentSentenceNum += 1;

        //0�Ԗڂ�\���ł���悤�ɂ���
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        //�I�����邩�����ԂŊm�F���Ă���
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }
}
