using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResouceScript : MonoBehaviour
{
    private GameObject go_BlockPosition;

    private ObjectElementText objectElementText;

    private int i_allResouce = 0;

    private bool b_compleate;

    public GameObject ResouceObject;

    [HideInInspector]
    public int i_resourceRecoveredSum; // �T�����ɉ�����������̐�

    public AudioClip audioClip;
    AudioSource audioSource;

    // UI
    [SerializeField]
    private Text resourceRecovered_Sum;
    private string defaultText;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!ResouceObject)
        {
            Debug.Log("ResouceScript (ResouceObject)�Q�Əo���܂���");
            return;
        }

        go_BlockPosition = GameObject.Find("MapCreateManager");

        // �z�u���鎑����
        i_allResouce = Random.Range(1, 6);

        //i_allResouce = objectElementText.GetShipParam().ResourceNum;

        //if (i_allResouce == 0)
        //{

        //    {
        //        i_allResouce = Random.Range(0, 6);
        //    }
        //}

        b_compleate = false;
    
        // �������������
        i_resourceRecoveredSum = 0;

        // UI
        defaultText = resourceRecovered_Sum.text;
        UpdateText(0);
    }

    private void Update()
    {
        if (!go_BlockPosition)
        {
            Debug.Log("ResouceScript (go_BlockPotision)�Q�Əo���܂���");
            return;
        }

        MapCreate mapCreate;

        mapCreate = go_BlockPosition.GetComponent<MapCreate>();

        if (!mapCreate)
        {
            Debug.Log("ResouceScript (mapCreate)�Q�Əo���܂���");
            return;
        }

        if (mapCreate.MapCreateCompleate())
        {
            if (!b_compleate)
            {
                for (int i = 0; i < i_allResouce; i++)
                {
                    int Rand = 2;

                    {
                        Rand = Random.Range(2, mapCreate.MapBlockList.Count);
                    }

                    Vector2 vec;

                    vec = mapCreate.MapBlockList[Rand].transform.position;

                    Instantiate(ResouceObject, vec, Quaternion.identity);
                    Debug.Log("�����z�u����");
                }
                b_compleate = true;
            }

        }
    }

    // ������� (�����m�ێ��ɌĂяo�����)
    public void AddResourceRecoveredSum()
    {
        audioSource.PlayOneShot(audioClip);
        Debug.Log("�����m�ۊ���");

        i_resourceRecoveredSum++;
        UpdateText(i_resourceRecoveredSum);
    }

    // �e�L�X�g���X�V
    void UpdateText(int resourceRecovered_Num)
    {
        resourceRecovered_Sum.text = defaultText + resourceRecovered_Num;
    }
}