using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Obtain : MonoBehaviour
{
    float fuelCnt = 0;
    float rsourceCnt = 0;
    public TextMeshProUGUI countText;
    public ItemManager itemManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            fuelCnt = fuelCnt + 1;
            Debug.Log("”R—¿{‚P");
            SetCountText();
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.tag == "Resource")
        {
            rsourceCnt = rsourceCnt + 1;
            Debug.Log("‘Œ¹{‚P");
            SetCountText2();
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
    }

    void SetCountText()
    {
        countText.text = "”R—¿‚ğŠl“¾‚µ‚½ " + fuelCnt.ToString();
    }
    void SetCountText2()
    {
        countText.text = "‘Œ¹‚ğŠl“¾‚µ‚½ " + rsourceCnt.ToString();
    }
}

