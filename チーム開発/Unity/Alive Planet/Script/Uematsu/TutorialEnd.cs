using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    //�`���[�g���A���̌��ɂȂ�e�I�u�W�F�N�g
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
