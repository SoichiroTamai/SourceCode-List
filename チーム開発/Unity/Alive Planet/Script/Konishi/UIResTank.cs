using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResTank : MonoBehaviour
{
    public MotherShip motherShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UIでのテキスト表示(残存量)
        Text resText = GetComponent<Text>();
        resText.text = "RESOURCE:" + motherShip.GetResource().ToString();
    }
}
