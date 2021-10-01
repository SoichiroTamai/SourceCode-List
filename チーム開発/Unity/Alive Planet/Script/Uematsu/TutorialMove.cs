using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : MonoBehaviour
{
    //�`���[�g���A���̉摜
    private GameObject TutorialImage;
    //�摜�Ɖ摜�̋���
    private const float DistanceImage = 720;

    //�摜�������Ă���̂��܂��A�ǂ��瑤�ɓ����Ă���̂�
    private bool MoveFlg = false;

    //�ړ��̃S�[���ƂȂ�ꏊ�̍��W
    private Vector3 StartPos;
    private Vector3 GorlPos;

    //��_�Ԃ̋���������
    private float distance_two;

    //�ړ��X�s�[�h
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        TutorialImage = GameObject.Find("TutorialImage_1");
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveFlg)
        {
            //���݂̈ʒu
            float present_Location = (Time.time * speed) / distance_two;

            //�I�u�W�F�N�g�̈ʒu
            TutorialImage.transform.position = Vector3.Lerp(StartPos, GorlPos, present_Location);

            //�S�[���܂œ������Ă����珈������߂�
            if(present_Location>=1.0f)
            {
                MoveFlg = false;
            }
        }
    }

    //Next�{�^���������ꂽ��
    public void OnButtonNext()
    {
        //�X�^�[�g�̈ʒu�ƃS�[���̈ʒu��ݒ肷��
        StartPos = TutorialImage.transform.position;
        GorlPos = StartPos;
        GorlPos.x -= DistanceImage;

        //�t���O�𗧂Ăď������X�^�[�g
        MoveFlg = true;
    }

    //Back�{�^���������ꂽ��
    public void OnButtonBack()
    {
        //�X�^�[�g�̈ʒu�ƃS�[���̈ʒu��ݒ肷��
        StartPos = TutorialImage.transform.position;
        GorlPos = StartPos;
        GorlPos.x += DistanceImage;

        //�t���O�𗧂Ăď������X�^�[�g
        MoveFlg = true;
    }
}
