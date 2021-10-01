using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// セーブボタンを押したときにセーブファイルのUIを3つ表示するスクリプト
public class UISaveActive : MonoBehaviour
{
    // 非表示にするオブジェクトのリスト
    public List<GameObject> UIActiveFalseList;

    // 表示するセーブファイルのリスト
    public List<GameObject> UIActiveTrueList;
    
    public void ButtonClick()
    {
        // 表示リストを表示
        for (int i = 0; i < UIActiveTrueList.Count; i++)
        {
            UIActiveTrueList[i].gameObject.SetActive(true);
        }

        // 非表示リストを非表示に
        for(int i = 0; i < UIActiveFalseList.Count; i++)
        {
            UIActiveFalseList[i].gameObject.SetActive(false);
        }
    }
}
