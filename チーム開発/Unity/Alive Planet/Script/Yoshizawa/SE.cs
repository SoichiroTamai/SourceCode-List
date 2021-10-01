using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    private AudioSource sound01;
    private AudioSource sound02;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];

        //sound01 = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        sound01.PlayOneShot(sound01.clip);
    }
    public void OnClick2()
    {
        sound02.PlayOneShot(sound02.clip);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    sound01.PlayOneShot(sound01.clip);
    //}
}
