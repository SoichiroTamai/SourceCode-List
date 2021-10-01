using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    //ボタンを押したらこの関数が動く
    public void OnClickStartButton()
    {
        //タイトルに戻る処理
        SceneManager.LoadScene("Title");
    }
}
