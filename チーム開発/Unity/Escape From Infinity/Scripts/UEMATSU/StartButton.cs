using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //ボタンを押せばこの関数を呼ぶ
    public void OnClickStartButton()
    {
        //ステージ1をロードするための処理
        SceneManager.LoadScene("StageOne");
    }
}
