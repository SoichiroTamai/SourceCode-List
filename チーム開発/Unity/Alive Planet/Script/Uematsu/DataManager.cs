using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //���ۂɃf�[�^���Z�[�u���Ă��鏊
    public SaveManager saveManager;

    public void OnClickSave()
    {
        saveManager.Save();
    }

    public void OnClickLoad()
    {
        saveManager.Load();
    }
}
