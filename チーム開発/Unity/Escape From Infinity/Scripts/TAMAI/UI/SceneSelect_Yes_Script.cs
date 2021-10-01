using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelect_Yes_Script : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        PauseScript.changeEndUI = true;
    }

    // Title を選択
    public void Select_Title()
    {
        PauseScript.sceneSelect = true;
    }

    // End を選択
    public void Select_End()
    {
        PauseScript.sceneSelect = false;
    }
}