using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextScript : MonoBehaviour
{
    private Text text;

    private string s_string;

    private bool b_score;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        b_score = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_score)
        {
            text.text = s_string;
            text.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else
        {
            text.text = null;
        }

    }

    public void SetText(int Score,string str)
    {
        s_string = str;
        s_string += Score.ToString();
        b_score = true;
    }

    internal void SetText(float resouce, string v)
    {
        throw new NotImplementedException();
    }
}
