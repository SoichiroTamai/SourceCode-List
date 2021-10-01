using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton_Script : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        if (PauseScript.changeTitleorEndUI)
        {
            PauseScript.changeTitleorEndUI = false;
        }
        else
        {
            // ポーズ画面を閉じる
            PauseScript.changePouse = true;
        }
    }
}