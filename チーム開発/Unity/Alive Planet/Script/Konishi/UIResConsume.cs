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
        // UIでのテキスト表示(使用量)
        Text resText = GetComponent<Text>();
        resText.text = "RESOURCE COUSUME : " + droneRepair.resSlider.value.ToString();
    }
}
