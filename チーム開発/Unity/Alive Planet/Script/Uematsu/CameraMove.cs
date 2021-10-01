using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //�J�����̍��W
    Transform Pos;
    //���C���J����
    Camera Cam;
    //�Q�[���}�l�[�W���[
    public GameMasterScript GameMaster;

    //�����X�s�[�h
    public float MoveSpeed = 0.05f;
    //�Y�[���̃L�[�R�[�h
    public KeyCode ZoomIn = KeyCode.I;
    public KeyCode ZoomOut = KeyCode.O;

    // Start is called before the first frame update
    private void Start()
    {
        //�A�^�b�`�����I�u�W�F�N�g�̍��W���擾
        Pos = this.gameObject.GetComponent<Transform>();
        //�A�^�b�`�����I�u�W�F�N�g�̃J�������擾
        Cam = this.gameObject.GetComponent<Camera>();

        //�J�����̈ʒu���v���C���[�ɍ��킹��
        Pos.position = Pos.position + new Vector3(GameMaster.PlayerPos.x, GameMaster.PlayerPos.y, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        Zoom();
    }

    //�J�������㉺���E�L�[�ňړ�
    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Pos.position = Pos.position + new Vector3(0.0f, MoveSpeed, 0.0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Pos.position = Pos.position - new Vector3(0.0f, MoveSpeed, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Pos.position = Pos.position - new Vector3(MoveSpeed, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Pos.position = Pos.position + new Vector3(MoveSpeed, 0.0f, 0.0f);
        }
    }

    //�w�肵���L�[�������ƃY�[���C���ƃY�[���A�E�g������
    private void Zoom()
    {
        if(Input.GetKey(ZoomIn))
        {
            Cam.orthographicSize = Cam.orthographicSize - MoveSpeed;
        }

        if (Input.GetKey(ZoomOut))
        {
            Cam.orthographicSize = Cam.orthographicSize + MoveSpeed;
        }
    }
}
