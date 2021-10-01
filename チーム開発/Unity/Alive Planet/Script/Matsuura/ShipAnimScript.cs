using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vector = new Vector3();

        vector.x -= 0.025f;

        transform.position += vector;
    }
}
