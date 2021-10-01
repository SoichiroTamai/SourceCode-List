using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorChange : MonoBehaviour
{
    public ObjectScript objScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // レイが当たるか掴んでたら
        if (objScript.RayCheck() || objScript.GetIsCatch())
        {
            // GameObjectの取得
            GameObject target = GameObject.Find("RayImage");
            // Imageの取得
            Image image = target.GetComponent<Image>();
            // 色の変更
            image.color = Color.green;
        }
        else
        {
            // GameObjectの取得
            GameObject target = GameObject.Find("RayImage");
            // Imageの取得
            Image image = target.GetComponent<Image>();
            // 色の変更
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
