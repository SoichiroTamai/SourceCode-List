using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoveButtons : MonoBehaviour
{
    // �J����
    public Camera cam;

    // �J�����̍��W
    Transform Pos;

    // �ړ����x
    float MoveSpeed = 0.05f;

    // ������Ă��邩
    bool UpButtonPush = false;
    bool DownButtonPush = false;
    bool LeftButtonPush = false;
    bool RightButtonPush = false;

    private void Start()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }

        // �J�����I�u�W�F�N�g�̍��W���擾
        Pos = cam.GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    // ���ꂼ��̃{�^����������Ă����Ԃ����擾����
    public void UpButtonPushDown()
    {
        UpButtonPush = true; 
    }

    public void UpButtonPushUp()
    {
        UpButtonPush = false;
    }

    public void DownButtonPushDown()
    {
        DownButtonPush = true;
    }

    public void DownButtonPushUp()
    {
        DownButtonPush = false;
    }

    public void LeftButtonPushDown()
    {
        LeftButtonPush = true;
    }

    public void LeftButtonPushUp()
    {
        LeftButtonPush = false;
    }

    public void RightPushDown()
    {
        RightButtonPush = true;
    }

    public void RightPushUp()
    {
        RightButtonPush = false;
    }

    // �ړ�����
    public void MoveUp()
    {
        Pos.position = Pos.position + new Vector3(0.0f, MoveSpeed, 0.0f);
    }

    public void MoveDown()
    {
        Pos.position = Pos.position - new Vector3(0.0f, MoveSpeed, 0.0f);
    }

    public void MoveLeft()
    {
        Pos.position = Pos.position - new Vector3(MoveSpeed, 0.0f, 0.0f);
    }

    public void MoveRight()
    {
        Pos.position = Pos.position + new Vector3(MoveSpeed, 0.0f, 0.0f);
    }

    // �J�������ړ�
    private void Move()
    {
        // �L�[�{�[�h����
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // UI�{�^������
        if (UpButtonPush)
        {
            MoveUp();
        }
        else if (DownButtonPush)
        {
            MoveDown();
        }
        else if (LeftButtonPush)
        {
            MoveLeft();
        }
        else if(RightButtonPush)
        {
            MoveRight();
        }
    }
}
