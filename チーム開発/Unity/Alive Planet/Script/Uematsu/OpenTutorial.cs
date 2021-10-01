using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    //チュートリアルのオブジェクト
    public GameObject tutorial;
    //チュートリアルが開かれたか
    public KeyCode openKey = KeyCode.M;
    //SEを鳴らす
    private AudioSource audioSource;
    public AudioClip SE;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンが押されたら音を鳴らしチュートリアルを開く
        if(Input.GetKeyDown(openKey))
        {
            tutorial.SetActive(true);
            Debug.Log("音鳴らしますよ");
            audioSource.PlayOneShot(SE);
        }
    }
}
