using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    public AudioClip SE;
    AudioSource audioSource;
    public DoubleSwitch DS1;
    public DoubleSwitch DS2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DS1.push)
        {     
           audioSource.Play();
        }
        if (DS2.push)
        {
            audioSource.Play();
        }
    }
}
