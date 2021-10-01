using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClickObject : MonoBehaviour
{
    //�N���b�N���ꂽ�I�u�W�F�N�g
    private GameObject ClickObject;

    //�N���b�N���ꂽ�I�u�W�F�N�g���͂��I�u�W�F�N�g
    public GameObject SelectBox;

    //�N���b�N�������GameObject����������?
    bool IsGameObject = false;

    //���b�Z�[�W���o���e�L�X�g�̃X�N���v�g
    ObjectElementText text;

    private void Start()
    {
        //�͂����ŏ��͏����Ă���
        SelectBox.SetActive(false);
        ClickObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClickObject();

        ////�Q�[���I�u�W�F�N�g���N���b�N����Έ͂���\��
        //if(IsGameObject)
        //{
        //    SelectBox.transform.position = ClickObject.transform.position;
        //    SelectBox.SetActive(true);
        //}
        //else
        //{
        //    SelectBox.SetActive(false);
        //}
    }

    void CheckClickObject()
    {
        //�}�E�X���N���b�N�����
        if (Input.GetMouseButtonDown(0))
        {
            //ClickObject = null;

            //���C���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //���C��������΃Q�[���I�u�W�F�N�g���i�[���͂���\���A�e�L�X�g�������ŏo���悤�ɂ���
            if(hit2d)
            {
                ClickObject = hit2d.transform.gameObject;
                IsGameObject = true;
                SelectBox.SetActive(true);
                SelectBox.transform.position = ClickObject.transform.position;
            }
            else
            {
                IsGameObject = false;
                Debug.Log("���C���������ĂȂ�");
            }
        }
    }

    //�N���b�N���ꂽ�I�u�W�F�N�g���΂��֐�
    public GameObject GetClickedObject()
    {
        return ClickObject;
    }
}
