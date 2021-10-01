using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutorSafe : MonoBehaviour
{
    //�N���b�N�����I�u�W�F�N�g
    public GetClickObject clickObject;

    //�f���̏��̃X�N���v�g
    private PlanetStatus planetScript;
    private bool click;

    private GameObject clickobject;

    int landing;

    public Text text;


    private void Start()
    {
        text = gameObject.AddComponent<Text>();

        clickobject = GameObject.Find("GetClickObject");
    }

    private void Update()
    {
        clickObject = clickobject.GetComponent<GetClickObject>();

        //�I�u�W�F�N�g���I������ĂȂ���ΕԂ�
        if (!clickObject.GetClickedObject())
        {
            text.text = "�L�^����";
            return;
        }

        //�I�����ꂽ�I�u�W�F�N�g�ɑΉ�����X�e�[�^�X��\������
        if (clickObject.GetClickedObject().tag == "Planet")
        {
            Planet(clickObject.GetClickedObject());
        }
    }

    public void Planet(GameObject planetObject)
    {
        Debug.Log("�L�^�Ȃ�");

        //��Ɠ���
        planetScript = planetObject.GetComponent<PlanetStatus>();
        int landing = planetScript.Probability;

        //���̃p�[�Z���g�Ő������邩�ǂ������o��
        int planet = planetScript.probability;
        Debug.Log(planet);
        //�������o���������O�ɏo�������̕����傫����ΐ���
        if (planet < landing)
        {
            text.text = "����!!!!!!!!!!!!!!!!!!!!!!!";
        }
        else
        {
            text.text = "���s!!!!!!!!!!!!!!!!!!!!!!!!";
        }
    }
}
