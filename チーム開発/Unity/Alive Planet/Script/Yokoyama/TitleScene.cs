using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    // 表示させるセーブファイル
    public List<Button> SaveFileList;

    public Button gameStart;

    public void ButtonClick()
    {
        // 非表示に
        gameStart.gameObject.SetActive(false);

        // ロード画面を表示
        for (int i = 0; i < SaveFileList.Capacity; i++)
        {
            SaveFileList[i].gameObject.SetActive(true);
        }
    }
}
