using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoDoor : MonoBehaviour
{
    public GameObject Door;

    public DoubleSwitch DS1;
    public DoubleSwitch DS2;

    private Vector3 v_OldDoorPos1;
    private Vector3 v_OldDoorPos2;

    private GameObject Cube1;
    private GameObject Cube2;

    // Start is called before the first frame update
    void Start()
    {
        Cube1 = Door.transform.Find("Cube.001").gameObject;
        Cube2 = Door.transform.Find("Cube.002").gameObject;
        v_OldDoorPos1 = Cube1.transform.position;
        v_OldDoorPos2 = Cube2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (DS1.push && DS2.push)
        {
            Vector3 vec1 = Cube1.transform.position;
            Vector3 vec2 = Cube2.transform.position;

            vec1.z += 0.4f;
            vec2.z -= 0.4f;

            Cube1.transform.position = vec1;
            Cube2.transform.position = vec2;
        }
        else
        {
          
            Cube1.transform.position = v_OldDoorPos1;
            Cube2.transform.position = v_OldDoorPos2;

        }
    }
}
