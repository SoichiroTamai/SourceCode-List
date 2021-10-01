using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonReturn : MonoBehaviour
{
    public RepairPadOpen repairPadOpen;
    [SerializeField] MotherShip motherShip;

    public void OnClick()
    {
        repairPadOpen.SetPadClosePos();

        // GameMaster�Ƀh���[�����X�g�𑗂�
        motherShip.SetDroneDataToGameMaster();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
