using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorlSound : MonoBehaviour
{ 
    public AudioClip gorlSE;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(gorlSE);
        }
    }
}
