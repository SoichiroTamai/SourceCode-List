using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRT_Active : MonoBehaviour
{
    public GameObject gameObject;

    public void DeletCRT()
    {
        gameObject.GetComponent<CRT>().enabled = false;
    }

    public void CreateCRT()
    {
        gameObject.GetComponent<CRT>().enabled = true;
    }
}
