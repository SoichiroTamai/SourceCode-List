using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSaveScene : MonoBehaviour
{
    //ボタンが押されたらセーブのシーンに移行
    public void OnClickButton()
    {
        SceneManager.LoadScene("DataSaveScene");
    }
}
