using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPanel : MonoBehaviour
{
    //パネルを登録する
    public GameObject Panel;

    // Start is called before the first frame update
    private void Start()
    {
        //パネルを消す
        this.gameObject.SetActive(false);
    }
}
