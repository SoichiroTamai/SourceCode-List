using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Flg : MonoBehaviour
{
    public bool is_Open; //ドアが開いたかどうかの変数
    public bool ItemFlg = true;

    public KeyCode keyCode;　//どのキーを入力するかの変数
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            //特定のボタンを押してかつis_Openがfalseの時、アイテムのフラグがtrue
            if (Input.GetKey(keyCode) && !is_Open && ItemFlg) is_Open = true;
        }

    }
}
