using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepairScene : MonoBehaviour
{
    GameObject WindowObj;
    GameObject TutorialObj;

    void Start()
    {
        WindowObj = GameObject.Find("Window");
        TutorialObj = GameObject.Find("Tutorial");
    }

    public void ButtonClick()
    {
        SceneManager.LoadScene("SelectRootScene");
    }

    private void Update()
    {
        
        if (WindowObj == GetComponent<GetClickedGameObject>().GetGameObject())
        {
            Debug.Log("‰ñ‚Á‚Ä‚Ü‚·");
            ButtonClick();
        }
    }
}
