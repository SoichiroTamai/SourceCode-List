using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    //�`���[�g���A���̃I�u�W�F�N�g
    public GameObject tutorial;
    //�`���[�g���A�����J���ꂽ��
    public KeyCode openKey = KeyCode.M;
    //SE��炷
    private AudioSource audioSource;
    public AudioClip SE;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�{�^���������ꂽ�特��炵�`���[�g���A�����J��
        if(Input.GetKeyDown(openKey))
        {
            tutorial.SetActive(true);
            Debug.Log("���炵�܂���");
            audioSource.PlayOneShot(SE);
        }
    }
}
