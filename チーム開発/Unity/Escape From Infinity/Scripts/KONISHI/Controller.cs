using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform‚ğæ“¾
        Transform myTransform = this.transform;

        // À•W‚ğæ“¾
        Vector3 pos = myTransform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.z += 0.005f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.z -= 0.005f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 0.005f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += 0.005f;
        }

        myTransform.position = pos;  // À•W‚ğİ’è
    }
}
