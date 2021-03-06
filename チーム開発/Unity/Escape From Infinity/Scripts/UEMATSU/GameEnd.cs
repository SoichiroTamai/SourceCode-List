using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void Quit()
    {
        #if UNITY_EDITOR
                //ゲームを終了させる処理
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void OnClickStartButton()
    {
        Quit();
    }
}
