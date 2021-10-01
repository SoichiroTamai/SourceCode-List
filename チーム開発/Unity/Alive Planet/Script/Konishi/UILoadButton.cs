using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoadButton : MonoBehaviour
{
    public SaveDataJson saveData;

    void Start()
    {
        // �ŏ��͔�\��
        gameObject.SetActive(false);
    }

    public void ButtonClick1()
    {
        // �Z�[�u�t�@�C�������[�h
        saveData.LoadGameData1();

        // OP�V�[���ɑJ��
        SceneManager.LoadScene("OPScene");
    }

    public void ButtonClick2()
    {
        // �Z�[�u�t�@�C�������[�h
        saveData.LoadGameData2();

        // OP�V�[���ɑJ��
        SceneManager.LoadScene("OPScene");
    }

    public void ButtonClick3()
    {
        // �Z�[�u�t�@�C�������[�h
        saveData.LoadGameData3();

        // OP�V�[���ɑJ��
        SceneManager.LoadScene("OPScene");
    }
}
