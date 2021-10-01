using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUIScoreFuel : MonoBehaviour
{
    [SerializeField] GameMasterScript gameMaster;
    public bool displayFlg = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(displayFlg)
        {
            Text scoreFuel = GetComponent<Text>();
            // ”R—¿Š”•\¦
            scoreFuel.text = "<color=#FF0000>GetFuel:" + gameMaster.FuelVault.ToString()
                + "</color>\n<color=#FFFFFF>Let's supplement it with the fuel which you obtained</color>";
        }
    }
}
