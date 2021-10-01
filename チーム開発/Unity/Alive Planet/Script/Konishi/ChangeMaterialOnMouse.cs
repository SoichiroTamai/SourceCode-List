using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnMouse : MonoBehaviour
{
    // 割り当てるマテリアル
    public Material[] materialList;
    // スプライトオブジェクトを変更するためのフラグ
    bool Change;
    
    // マウスカーソルがオブジェクトに重なった時
    private void OnMouseEnter()
    {
        Debug.Log("タッチ");
        changeMaterial();
    }

    // マウスカーソルがオブジェクトから離れたとき
    private void OnMouseExit()
    {
        changeMaterial();
    }

    // マテリアル変更
    public void changeMaterial()
    {
        // スプライトオブジェクトの変更フラグが true の場合
        if (Change)
        {
            // スプライトオブジェクトの変更
            //（配列 m_Sprite[0] に格納したスプライトオブジェクトを変数 m_spriteRenderer に格納したSpriteRenderer コンポーネントに割り当て）
            GetComponent<Renderer>().material = materialList[0];
            Change = false;
        }
        // スプライトオブジェクトの変更フラグが false の場合
        else
        {
            // スプライトオブジェクトの変更
            //（配列 m_Sprite[1] に格納したスプライトオブジェクトを変数 m_spriteRenderer に格納したSpriteRenderer コンポーネントに割り当て）
            GetComponent<Renderer>().material = materialList[1];
            Change = true;
        }
    }

    void Start()
    {
        // スプライトオブジェクトを変更するためのフラグを false に設定
        Change = false;
    }
}
