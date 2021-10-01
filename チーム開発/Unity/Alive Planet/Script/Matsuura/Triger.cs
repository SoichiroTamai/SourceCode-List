using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triger : MonoBehaviour
{
    public bool triger;

    // Start is called before the first frame update
    void Start()
    {
        triger = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "MapTile") { return; }
        triger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "MapTile") { return; }
        triger = false;
    }
}
