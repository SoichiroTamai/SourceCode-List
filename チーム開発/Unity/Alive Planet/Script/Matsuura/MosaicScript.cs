using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosaicScript : MonoBehaviour
{
    private GameObject OBJAlpha;

    private GameObject mosaiclist;

    private bool b_colorDelete;

    private float f_alpha;

    private void Start()
    {
        b_colorDelete = false;
        f_alpha = 0.95f;
        OBJAlpha = this.gameObject;
        mosaiclist = GameObject.Find("MapCreateManager");
    }

    private void Update()
    {
        if (!mosaiclist)
        {
            Debug.Log("MosaicScriptéQè∆é∏îs");
            return;
        }

        if (b_colorDelete)
        {
            f_alpha -= 0.001f;
        }

        OBJAlpha.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, f_alpha);

        if (f_alpha < 0.0f)
        {
            Destroy(this.gameObject);
            mosaiclist.GetComponent<MapCreate>().MosaicList.RemoveAt(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Drone") { return; }

        b_colorDelete = true;
    }
}
