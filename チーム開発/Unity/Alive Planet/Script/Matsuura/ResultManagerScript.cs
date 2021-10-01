using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManagerScript : MonoBehaviour
{
    public GameObject ResoucesText;
    public GameObject DroneText;
    public GameObject FuelText;

    private ShipParameter S_ResultParamater=new ShipParameter();

    private bool b_complete;

    // Start is called before the first frame update
    private void Start()
    {
        b_complete = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!ResoucesText) { return; }
        if (!DroneText) { return; }
        if (!FuelText) { return; }

        if (b_complete)
        {

            ResultTextScript RTScript;

            RTScript = ResoucesText.GetComponent<ResultTextScript>();

            RTScript.SetText(S_ResultParamater.resouce,"�������@");

            RTScript = DroneText.GetComponent<ResultTextScript>();

            RTScript.SetText(S_ResultParamater.drone,"�h���[�����@");

            //RTScript = FuelText.GetComponent<ResultTextScript>();

           // RTScript.SetText(S_ResultParamater.fuel,"�R�����@");

            b_complete = false;
        }
    }

    public void SetResultResouce(GameMasterScript gmScript)
    {
        S_ResultParamater = gmScript.shipParameter;
        b_complete = true;
    }
}
