using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    GameObject switch_Base;
    private Switch_Flg switch_Flg;
    private bool is_Open;
    // Start is called before the first frame update
    void Start()
    {
        switch_Base = GameObject.Find("Switch");
        //Switch_Base‚ÌSwitch_Flag‚ğæ“¾
        switch_Flg = switch_Base.GetComponent<Switch_Flg>();
    }

    // Update is called once per frame
    void Update()
    {
        is_Open = switch_Flg.is_Open;@//Switch_Flag‚©‚çis_Open‚ğæ“¾

        if (is_Open)
        {
            this.gameObject.transform.Translate(0, 0.05f, 0);
        }
    }
}
