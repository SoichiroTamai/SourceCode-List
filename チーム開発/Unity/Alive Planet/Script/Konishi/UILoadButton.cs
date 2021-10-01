using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoadButton : MonoBehaviour
{
    public SaveDataJson saveData;

    void Start()
    {
        // 最初は非表示
        gameObject.SetActive(false);
    }

    public void ButtonClick1()
    {
        // セーブファイルをロード
        saveData.LoadGameData1();

        // OPシーンに遷移
        SceneManager.LoadScene("OPScene");
    }

    public void ButtonClick2()
    {
        // セーブファイルをロード
        saveData.LoadGameData2();

        // OPシーンに遷移
        SceneManager.LoadScene("OPScene");
    }

    public void ButtonClick3()
    {
        // セーブファイルをロード
        saveData.LoadGameData3();

        // OPシーンに遷移
        SceneManager.LoadScene("OPScene");
    }
}
