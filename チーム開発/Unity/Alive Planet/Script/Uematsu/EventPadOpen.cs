using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPadOpen : MonoBehaviour
{
    GameObject eventPadObj;
    bool buttonFlg = false;
    float speed = 0.005f;
    float progress = 0.0f;
    bool endFlg = true;
    Vector3 startPos;
    Vector3 goalPos;

    public bool GetEndFlg() { return endFlg; }
    public bool GetButtonFlg() { return buttonFlg; }

    //�J������؂�ւ��邽�߂Ƀ��C���̃J�����ƃT�u�̃J����
    private GameObject mainCamera;
    private GameObject subCamera;

    //�����������Ă������߂̃I�u�W�F�N�g
    private GameObject canvas;

    void SetPadOpenPos()
    {
        if (eventPadObj == GetComponent<GetClickedGameObject>().GetGameObject())
        {
            Debug.Log("�N���b�N");

            buttonFlg = true;

            // transform���擾
            Transform myTransform = this.transform;

            // ���W���擾
            Vector3 pos = myTransform.position;

            startPos = pos;

            goalPos = pos;

            goalPos.x -= 30;

            GetComponent<GetClickedGameObject>().DeleteGameObject();
        }
    }

    public void SetPadClosePos()
    {
        buttonFlg = false;

        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;

        startPos = pos;

        goalPos = pos;

        startPos.x += 30;

        ScreenErase();
    }

    void PadOpen()
    {
        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;

        Vector3 vStart = startPos;
        Vector3 vGoal = goalPos;

        //�ڕW�n�_�܂ł̃x�N�g��
        Vector3 vTo = vGoal - vStart;

        //�i�s����������Č��݂̒n�_������o��
        Vector3 vNow = vStart + vTo * Mathf.Sin((progress * Mathf.PI) / 2);

        //�n�_�𒆊Ԃɍ��킹��
        pos = vNow;

        //�i�s��̍X�V
        progress += speed;
        if (progress >= 1.0f)
        {
            progress = 1.0f;
            endFlg = true;
            ScreenStartUP();
        }
        else
        {
            endFlg = false;
        }

        myTransform.position = pos;  // ���W��ݒ�
    }

    void PadClose()
    {
        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;

        Vector3 vStart = startPos;
        Vector3 vGoal = goalPos;

        //�ڕW�n�_�܂ł̃x�N�g��
        Vector3 vTo = vGoal - vStart;

        //�i�s����������Č��݂̒n�_������o��
        Vector3 vNow = vStart + vTo * Mathf.Sin((progress * Mathf.PI) / 2);

        //�n�_�𒆊Ԃɍ��킹��
        pos = vNow;

        //�i�s��̍X�V
        progress -= speed;
        if (progress <= 0.0f)
        {
            progress = 0.0f;
            endFlg = true;
        }
        else
        {
            endFlg = false;
        }

        myTransform.position = pos;  // ���W��ݒ�
    }

    //�C����ʂ���������(�X�^�[�g��)
    void ScreenEraseStart()
    {
        canvas.SetActive(false);
    }

    //�C����ʂ���������(�X�^�[�g���ȊO)
    void ScreenErase()
    {
        canvas.SetActive(false);
        mainCamera.SetActive(true);
        subCamera.SetActive(false);
    }

    //�C����ʂ�t���鏈��
    void ScreenStartUP()
    {
        canvas.SetActive(true);
        mainCamera.SetActive(false);
        subCamera.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        eventPadObj = GameObject.Find("EventPadObj");

        canvas = GameObject.Find("EventCanvas");
        mainCamera = GameObject.Find("Main Camera");
        subCamera = GameObject.Find("CRT Camera");

        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;

        startPos = pos;

        goalPos = pos;

        goalPos.x -= 30;

        ScreenEraseStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (endFlg)
        {
            if (!buttonFlg)
            {
                SetPadOpenPos();
            }
        }

        if (buttonFlg)
        {
            PadOpen();
        }
        else
        {
            PadClose();
        }
    }
}
