using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManage : MonoBehaviour
{
    public bool AppearanceEvent;

    public GameObject motherShip;

    public GameObject TextObject;

    private float f_Resouce;

    private float f_Fuel;

    private int i_TotalEvents;

    private int i_EventNumber;

    private bool b_getDownKey;

    private MotherShip MS_motherShip;

    private MotherShip.Param MS_P_Param;

    private Text t_eventText;

    private Text t_calcResouceText;

    void Start()
    {
        AppearanceEvent = false;
        i_TotalEvents = 10;
        i_EventNumber = 0;
        b_getDownKey = false;

        t_eventText = this.GetComponent<Text>();
        t_eventText.color = new Color(0, 1, 0);

        t_calcResouceText = TextObject.GetComponent<Text>();

        MS_motherShip = motherShip.GetComponent<MotherShip>();

        MS_P_Param = MS_motherShip.GetResouceParam();

        AppearanceEvent = true;
        b_getDownKey = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!b_getDownKey)
            {
                AppearanceEvent = true;
                b_getDownKey = true;
            }
        }
        else
        {
            b_getDownKey = false;
        }

        if (AppearanceEvent)
        {
            i_EventNumber = Random.Range(0, i_TotalEvents);
            EventContents();
            AppearanceEvent = false;
        }


        MS_P_Param = MS_motherShip.GetResouceParam();
    }

    public void EventContents()
    {
        //if (f_Resouce <= 0) { return; }

        f_Resouce = MS_P_Param.p_resouce;
        f_Fuel = MS_P_Param.p_fuel;

        switch (i_EventNumber)
        {
            case 0:
                Debug.Log("�����N���Ȃ�����");
                t_eventText.text = "�����N���Ȃ�����";
                f_Resouce -= 0.0f;
                t_calcResouceText.text = "���ʁF"+ f_Resouce;
                break;
            case 1:
                Debug.Log("�l�Y�~�Ɏ��������ꂽ�I");
                t_eventText.text = "�l�Y�~�Ɏ��������ꂽ�I";
                f_Resouce -= 2.0f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 2:
                Debug.Log("�������������Ă����I");
                t_eventText.text = "�������������Ă����I";
                f_Resouce -= 1.0f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 3:
                Debug.Log("�D�̃����e�i���X�����Ȃ����");
                t_eventText.text = "�D�̃����e�i���X�����Ȃ����";
                f_Resouce -= 1.5f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 4:
                Debug.Log("�h���[���̌̏Ⴊ�����I");
                t_eventText.text = "�h���[���̌̏Ⴊ�����I";
                f_Resouce -= 2.5f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 5:
                Debug.Log("�����̐ߖ�ɐ���");
                t_eventText.text = "�����̐ߖ�ɐ���";
                f_Resouce += 1.0f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 6:
                Debug.Log("�q�H�̃X�y�[�X�f�u���̒��Ɏ����R���e�i�𔭌�");
                t_eventText.text = "�q�H�̃X�y�[�X�f�u���̒��Ɏ����R���e�i�𔭌�";
                f_Resouce += 3.0f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 7:
                Debug.Log("����~�X�Ŏ������F����Ԃɗ��Ƃ��Ă��܂���");
                t_eventText.text = "����~�X�Ŏ������F����Ԃɗ��Ƃ��Ă��܂���";
                f_Resouce -= 0.5f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 8:
                Debug.Log("�g�����ɂȂ�Ȃ������𕪉����Ă������g���镔�i���l������");
                t_eventText.text = "�g�����ɂȂ�Ȃ������𕪉����Ă������g���镔�i���l������";
                f_Resouce += 0.5f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            case 9:
                Debug.Log("�����̊��p�Ɏ��s");
                t_eventText.text = "�����̊��p�Ɏ��s";
                f_Resouce -= 3.0f;
                t_calcResouceText.text = "���ʁF" + f_Resouce;
                break;
            default:
                Debug.Log("�\�������͂����Ȃ�");
                t_eventText.text = "�\�������͂����Ȃ�";
                break;
        }

        if (f_Fuel <= 0) { return; }


    }
}
