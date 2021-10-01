using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeObject : MonoBehaviour
{
    public GameObject SumUp;
    private bool DeleteFlg = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        //SumUp = GameObject.Find("SumUpObject");
        //DeleteFlg = true;
    }

    //private void Update()
    //{
    //    if (DeleteFlg)
    //    {
    //        if (SceneManager.GetActiveScene().name == "SelectRootScene")
    //        {
    //            SumUp.SetActive(true);
    //        }
    //        else
    //        {
    //            Debug.Log("è¡ÇµÇ‹Å[Ç∑");
    //            SumUp.SetActive(false);
    //            DeleteFlg = false;
    //        }
    //    }
    //}
}
