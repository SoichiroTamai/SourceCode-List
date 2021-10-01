using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    //チュートリアルの元になる親オブジェクト
    private GameObject TutorialObject;
    public GameMasterScript masterScript;

    // Start is called before the first frame update
    void Start()
    {
        TutorialObject = GameObject.Find("Tutorial");
    }

    public void OnButton()
    {
        masterScript.Tutorial = false;
        TutorialObject.SetActive(false);
    }
}
