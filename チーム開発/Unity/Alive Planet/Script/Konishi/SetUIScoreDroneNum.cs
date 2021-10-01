using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUIScoreDroneNum : MonoBehaviour
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
        if (displayFlg)
        {
            Text scoreDroneNum = GetComponent<Text>();
            // ƒhƒ[ƒ“‚ÌŠ”•\¦
            scoreDroneNum.text = "<color=#FF0000>GetDrone:" + gameMaster.GetDroneDataList().Count.ToString()
                + "</color>\n<color=#FFFFFF>Let's search in the drone which you obtained</color>";
        }
    }
}
