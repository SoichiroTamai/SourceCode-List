using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text1;
    public Text text2;
    public GetClickObject clickObject;

    private OutorSafe outsafe;
    public GameObject gm;

    void Start()
    {
        text1 = gameObject.GetComponent<Text>();
        text2 = gameObject.GetComponent<Text>();
        outsafe = gm.GetComponent<OutorSafe>();
    }

    // Update is called once per frame
    void Update()
    {

        text1 = outsafe.text;
        Debug.Log(text1.text);

        text2.text = text1.text;
    }
}
