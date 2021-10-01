using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGimmic : MonoBehaviour
{
    public GameObject one;
    public GameObject two;

    private Quaternion q_oneRot;
    private Quaternion q_twoRot;

    public GameObject Door;

    private bool OneFlg;

    private bool twoFlg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!one && !two) { return; }

        if(one.transform.rotation!=q_oneRot)
        {
            OneFlg = true;
        }
        else
        {
            OneFlg = false;
        }

        if(two.transform.rotation!=q_twoRot)
        {
            twoFlg = true;
        }
        else
        {
            twoFlg = false;
        }

        if(OneFlg&&twoFlg)
        {

            Vector3 vec = Door.transform.position;

            vec.y += 0.05f;

            Door.transform.position = vec;
        }


        q_oneRot = one.transform.rotation;
        q_twoRot = two.transform.rotation;
    }
}
