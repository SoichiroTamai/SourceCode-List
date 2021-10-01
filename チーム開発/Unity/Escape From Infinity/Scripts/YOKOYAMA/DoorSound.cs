using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    public GameObject Door;

    public DoubleSwitch DS1;
    public DoubleSwitch DS2;

    private bool doorFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        Door = GameObject.Find("Door");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DS1.push && DS2.push)
        {
            if(!doorFlg)
            {
                doorFlg = true;
                
            }
            if (doorFlg)
            {
                audioSource.PlayOneShot(sound1);
               
            }
        }
    }
}
