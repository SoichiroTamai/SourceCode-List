using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion raw=new Quaternion(0,0,0,0);

        raw.z += 0.1f;
        raw.x += 0.1f;
        raw.y += 0.1f;

        this.gameObject.transform.RotateAround(new Vector3(0, 0, 0.01f), new Vector3(-1, 0, 0), 5);
        Debug.Log("‰ñ‚Á‚Ä‚é");
    }
}
