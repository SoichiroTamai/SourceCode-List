using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    //�X�N���[�����n�܂������̃t���O
    private bool ScrollStartFlg = false;
    //�X�N���[���̋N�_�ƂȂ���W
    private Vector2 scrollSrartPos = new Vector2();
    //���E�̈ړ�����
    public static float SCROLL_END_LEFT = 10;
    public static float SCROLL_END_RIGHT = -10;
    //�X�N���[������
    public static float SCROLL_DISTANCE_CORRECTION = 0.001f;

    //�^�b�`�|�W�V�����̏�����
    private Vector2 ClickPosition = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //���C���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (ScrollStartFlg==false&&hit2d)
            {
                //�^�b�`�ʒu�ɃI�u�W�F�N�g���������瓮���Ȃ�
                //�X�N���[���ƃI�u�W�F�N�g�^�b�`�̏����𕪂���
                return;
            }
            else
            {
                //�^�b�`�����ꏊ�ɉ����Ȃ���΃X�N���[���J�n
                ScrollStartFlg = true;
                //X�������ɓ����Ă��Ȃ����
                if(scrollSrartPos.x==0.0f)
                {
                    //�X�N���[���J�n�ʒu���擾(�}�E�X�̃��[���h���W)
                    scrollSrartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    //�N���b�N�����ʒu���i�[
                    Vector2 ClickMovePos = ClickPosition;

                    if (scrollSrartPos.x != ClickMovePos.x) 
                    {
                        //���O�̃^�b�`�ʒu�Ƃ̍����i�[����
                        float diffPos = SCROLL_DISTANCE_CORRECTION * (ClickMovePos.x - scrollSrartPos.x);

                        //�J�����̍��W
                        Vector2 Pos = this.transform.position;
                        //�^�b�`�ʒu�Ƃ̍����J�������W�𓮂���
                        Pos.x -= diffPos;

                        //�X�N���[���������𒴉߂���ꍇ�������~�߂�
                        //if(Pos.x>SCROLL_END_RIGHT||Pos.x<SCROLL_END_LEFT)
                        //{
                        //    return;
                        //}

                        //�J�����̍��W���v�Z�������ړ�
                        this.transform.position = Pos;
                        //�X�N���[���J�n�ʒu��ύX���A���̃X�N���[���ɔ�����
                        scrollSrartPos = ClickMovePos;
                    }
                }
            }
        }
        else
        {
            //�^�b�`�𗣂�����t���O�𗎂Ƃ��A�X�N���[���J�n�ʒu������������
            ScrollStartFlg = false;
            scrollSrartPos = new Vector2();
        }
    }
}
