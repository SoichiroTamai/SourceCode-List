using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClickObject : MonoBehaviour
{
    //クリックされたオブジェクト
    private GameObject ClickObject;

    //クリックされたオブジェクトを囲うオブジェクト
    public GameObject SelectBox;

    //クリックした先にGameObjectがあったか?
    bool IsGameObject = false;

    //メッセージを出すテキストのスクリプト
    ObjectElementText text;

    private void Start()
    {
        //囲いを最初は消しておく
        SelectBox.SetActive(false);
        ClickObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClickObject();

        ////ゲームオブジェクトをクリックすれば囲いを表示
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
        //マウスをクリックすれば
        if (Input.GetMouseButtonDown(0))
        {
            //ClickObject = null;

            //レイを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //レイが当たればゲームオブジェクトを格納し囲いを表示、テキストもここで出すようにする
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
                Debug.Log("レイが当たってない");
            }
        }
    }

    //クリックされたオブジェクトを飛ばす関数
    public GameObject GetClickedObject()
    {
        return ClickObject;
    }
}
