using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void Quit()
    {
        #if UNITY_EDITOR
                //ƒQ[ƒ€‚ğI—¹‚³‚¹‚éˆ—
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
