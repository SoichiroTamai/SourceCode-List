using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNoButton_Script : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        PauseScript.changeEndUI = false;
    }
}