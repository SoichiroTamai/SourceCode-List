using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSaveScene : MonoBehaviour
{
    //�{�^���������ꂽ��Z�[�u�̃V�[���Ɉڍs
    public void OnClickButton()
    {
        SceneManager.LoadScene("DataSaveScene");
    }
}
