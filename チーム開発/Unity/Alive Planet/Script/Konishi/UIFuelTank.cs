using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFuelTank : MonoBehaviour
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
        Text fuelText = GetComponent<Text>();
        fuelText.text = "FUEL:" + motherShip.GetFuel().ToString();
    }
}
