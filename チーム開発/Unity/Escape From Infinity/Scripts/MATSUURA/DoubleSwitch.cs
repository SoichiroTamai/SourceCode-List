using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSwitch : MonoBehaviour
{
    private Vector3 v_oldPos;

    private Rigidbody rd;

    public bool push;

    private float i_Time = 4;

    private bool b_Check;

    private bool b_back;

    AudioSource switchSE;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        push = false;
        rd = GetComponent<Rigidbody>();
        v_oldPos = transform.position;

        switchSE = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (b_Check)
        {
            i_Time -= Time.deltaTime;
            if(i_Time<0)
            {
                b_back = true;
            }
        }
        else
        {
            transform.position = v_oldPos;
        }
        if (b_back)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            b_Check = false;
            push = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    // “–‚½‚è”»’è’†////////////////////////////////////
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            b_back = false;
            i_Time = 4;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            push = true;
            switchSE.Play();
        }
    }

    // “–‚½‚è”»’è‚©‚ç—£‚ê‚½Žž////////////////////////////
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            b_Check = true;
        }

    }
}
