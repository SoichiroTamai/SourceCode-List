using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUIScoreResource : MonoBehaviour
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
            Text scoreResource = GetComponent<Text>();
            // éëåπèäéùêîï\é¶
            scoreResource.text = "<color=#FF0000>GetResource:" + gameMaster.ResouceVault.ToString()
                + "</color>\n<color=#FFFFFF>Let's repair a drone in the resources which you obtained</color>";
        }
    }
}
