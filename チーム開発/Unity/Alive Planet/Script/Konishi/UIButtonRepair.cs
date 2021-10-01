using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonRepair : MonoBehaviour
{
    public DroneRepair droneRepair;

    public void OnClick()
    {
        droneRepair.Repair();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<GetClickedGameObject>().GetGameObject()) { return; }
        droneRepair = GetComponent<GetClickedGameObject>().GetGameObject().GetComponent<DroneRepair>();
    }
}
