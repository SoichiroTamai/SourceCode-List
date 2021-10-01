using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlphaChange : MonoBehaviour
{
    // 透明度
    float alpha;
    public MöbiusSystem möbiusSystem;

    // Start is called before the first frame update
    void Start()
    {
        // 透明度設定
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // GameObjectの取得
        GameObject target = GameObject.Find("MöbiusImage");
        // Imageの取得
        Image image = target.GetComponent<Image>();
        // 透明度上昇
        alpha = Mathf.Lerp(1, 0, 1 - möbiusSystem.seconds/10);
        //Debug.Log(möbiusSystem.seconds/10);
        // 0=透明 1=不透明なので、1.0で完全に不透明になる
        image.SetOpacity(alpha);
    }
}
