using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndYesButton_Script : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        // Title を選択している時
        if (PauseScript.sceneSelect)
        {
            // 内部時間を稼働
            Time.timeScale = 1.0f;

            // マウスカーソルを表示する
            Cursor.visible = true;

            //タイトルに戻る
            SceneManager.LoadScene("Title");
        }
        // ゲーム終了時
        else
        {
            #if UNITY_EDITOR
                            //ゲームを終了させる処理
                            UnityEditor.EditorApplication.isPlaying = false;
            #else
                        Application.Quit();
            #endif
        }
    }
}