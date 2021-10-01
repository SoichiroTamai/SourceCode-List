using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //実際にデータをセーブしている所
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
