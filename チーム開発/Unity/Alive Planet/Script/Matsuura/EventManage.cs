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
                Debug.Log("何も起きなかった");
                t_eventText.text = "何も起きなかった";
                f_Resouce -= 0.0f;
                t_calcResouceText.text = "結果："+ f_Resouce;
                break;
            case 1:
                Debug.Log("ネズミに資源を喰われた！");
                t_eventText.text = "ネズミに資源を喰われた！";
                f_Resouce -= 2.0f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 2:
                Debug.Log("資源がくさっていた！");
                t_eventText.text = "資源がくさっていた！";
                f_Resouce -= 1.0f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 3:
                Debug.Log("船のメンテナンスをしなければ");
                t_eventText.text = "船のメンテナンスをしなければ";
                f_Resouce -= 1.5f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 4:
                Debug.Log("ドローンの故障が発生！");
                t_eventText.text = "ドローンの故障が発生！";
                f_Resouce -= 2.5f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 5:
                Debug.Log("資源の節約に成功");
                t_eventText.text = "資源の節約に成功";
                f_Resouce += 1.0f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 6:
                Debug.Log("航路のスペースデブリの中に資源コンテナを発見");
                t_eventText.text = "航路のスペースデブリの中に資源コンテナを発見";
                f_Resouce += 3.0f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 7:
                Debug.Log("操作ミスで資源を宇宙空間に落としてしまった");
                t_eventText.text = "操作ミスで資源を宇宙空間に落としてしまった";
                f_Resouce -= 0.5f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 8:
                Debug.Log("使い物にならない資源を分解していくつか使える部品を獲得した");
                t_eventText.text = "使い物にならない資源を分解していくつか使える部品を獲得した";
                f_Resouce += 0.5f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            case 9:
                Debug.Log("資源の活用に失敗");
                t_eventText.text = "資源の活用に失敗";
                f_Resouce -= 3.0f;
                t_calcResouceText.text = "結果：" + f_Resouce;
                break;
            default:
                Debug.Log("表示されるはずがない");
                t_eventText.text = "表示されるはずがない";
                break;
        }

        if (f_Fuel <= 0) { return; }


    }
}
