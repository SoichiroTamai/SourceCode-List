using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRepairPadOpen : MonoBehaviour
{
    GameObject repairPadObj;

    bool buttonFlg = false;
    float speed = 0.005f;
    float progress = 0.0f;
    bool endFlg = true;
    float move = 1200;
    Vector3 startPos;
    Vector3 goalPos;

    void SetPadOpenPos()
    {
        if (repairPadObj == GetComponent<GetClickedGameObject>().GetGameObject())
        {
            buttonFlg = true;

            // transform���擾
            Transform myTransform = this.transform;

            // ���W���擾
            Vector3 pos = myTransform.position;

            startPos = pos;

            goalPos = pos;

            goalPos.x -= move;

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

        startPos.x += move;
    }

    // Start is called before the first frame update
    void Start()
    {
        repairPadObj = GameObject.Find("RepairPadObj");

        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;

        startPos = pos;

        goalPos = pos;

        goalPos.x -= move;
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
            }
            else
            {
                endFlg = false;
            }

            myTransform.position = pos;  // ���W��ݒ�
        }
        else
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
    }
}
