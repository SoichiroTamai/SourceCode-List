using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameMasterScript masterScript;

    // Start is called before the first frame update
    void Start()
    {
        if(masterScript.Tutorial)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }
}
