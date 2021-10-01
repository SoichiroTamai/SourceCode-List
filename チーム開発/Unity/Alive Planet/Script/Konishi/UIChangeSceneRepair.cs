using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChangeSceneRepair : MonoBehaviour
{
    [SerializeField] GameObject changeSceneButton;
    [SerializeField] RepairPadOpen repairPadOpen;

    public void ButtonClick()
    {
        SceneManager.LoadScene("SelectRootScene");
    }

    private void Update()
    {
        if (!repairPadOpen.GetButtonFlg())
        {
            if (repairPadOpen.GetEndFlg())
            {
                changeSceneButton.SetActive(true);
            }
        }
        else
        {
            changeSceneButton.SetActive(false);
        }
    }
}
