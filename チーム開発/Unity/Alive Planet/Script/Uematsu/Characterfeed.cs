using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characterfeed : MonoBehaviour
{
    public string[] sentences;//�������i�[����
    public Sprite[] BackTexture;//�C���[�W���i�[����
    public string[] spriteName;//�C���[�W�̖��O��K������
    public SpriteRenderer uiBackTexture;//�w�i�̃e�N�X�`���̎Q��
    public SpriteRenderer uiFrontTexture;//���ʂ̃e�N�X�`���̎Q��
    [SerializeField] Text uitext;//uitext�ւ̎Q��

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;//1�����̕\���ɂ����鎞��

    private int currentSentenceNum = 0;//���ݕ\�����Ă��镶�͔ԍ�
    private string currentSentence = string.Empty;//���݂̕�����
    private float timeUntilDisplay = 0;//�\���ɂ����鎞��
    private float timeBeganDisplay = 1;//������̕\�����J�n��������
    private int lastUpdateCharCount = -1;//���ݕ\�����̕�����

    //��ʊO�̓������n�߂̈ʒu
    private Vector3 NewPos = new Vector3(500, 0);
    //��ʈړ��̃X�^�[�g�̏ꏊ�ƏI���̏ꏊ
    private Vector3 startPos;
    private Vector3 gorlPos;
    float progress = 0.0f;

    //��ʂ̃t�F�[�h�C���A�E�g�̊Ǘ�
    public FadeController fadeController;

    //�R�����Z�b�g
    public GameMasterScript masterScript;
    //�����R��
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
        //�����̕\������/������
        if(IsDisplayComplete())
        {
            
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
            if(Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }

            //���ʂ̉摜���ړ�������
            uiFrontTexture.transform.position = LerpMoveImage(uiFrontTexture.transform.position, NewPos,
                uiBackTexture.transform.position);
        }

        //���\������镶�������v�Z
        int DisplayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //�������v�Z���������������ݕ\�����Ă��镶�����ƈႦ��
        if(DisplayCharCount!=lastUpdateCharCount)
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
        //�X�N���[���̃X�^�[�g�ʒu��ύX����
        uiFrontTexture.transform.position = NewPos;
        Debug.Log(uiFrontTexture.transform.position);
        //�ϐ��̌^��ς���
        uiBackTexture.GetComponent<SpriteRenderer>();
        uiFrontTexture.gameObject.GetComponent<SpriteRenderer>();
        //�i�[����Ă���摜��ύX����
        uiBackTexture.sprite = BackTexture[currentSentenceNum];
        uiFrontTexture.sprite = BackTexture[currentSentenceNum + 1];
        
        //���݂̌o�ߎ��Ԃ��J�n���ԂƂ��ĕێ����Ă���
        timeBeganDisplay = Time.time;
        currentSentenceNum += 1;

        Debug.Log(currentSentence.Length);

        //0�Ԗڂ�\���ł���悤�ɂ���
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        //�I�����邩�����ԂŊm�F���Ă���
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }

    Vector3 LerpMoveImage(Vector3 pos,Vector3 start,Vector3 end)
    {
        //�J�n�n�_�ƏI���n�_��ݒ�
        Vector3 vStartPos = start;
        Vector3 vEndPos = end;

        //�ړI�n�܂ł̃x�N�g��
        Vector3 vTo = vEndPos - vStartPos;

        //�i�s����������Č��݂̒n�_������o��
        Vector3 vNow = vStartPos + vTo * Mathf.Sin((progress * Mathf.PI) / 2);

        //�n�_�𒆊Ԃɍ��킹��
        pos = vNow;

        //�i�s���̍X�V
        progress = Mathf.Clamp01((Time.time / (timeBeganDisplay + timeUntilDisplay)));

        //Debug.Log(pos);

        return pos;
    }
}
