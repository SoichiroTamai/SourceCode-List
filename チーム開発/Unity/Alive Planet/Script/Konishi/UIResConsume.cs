using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResConsume : MonoBehaviour
{
    public DroneRepair droneRepair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UI�ł̃e�L�X�g�\��(�g�p��)
        Text resText = GetComponent<Text>();
        resText.text = "RESOURCE COUSUME : " + droneRepair.resSlider.value.ToString();
    }
}
