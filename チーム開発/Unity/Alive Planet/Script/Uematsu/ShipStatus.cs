using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatus : MonoBehaviour
{
    //‘Œ¹‚Ì—Ê
    public int ResourceNum;
    //”R—¿‚Ì—Ê
    public int FuelNum;

    //‘Ş‚Æ”R—¿‚Ì‡ŒvŒÂ”
    private int AddStatus = 0;

    //‘Ş‚Ì—Ê‚ğì¬‚·‚éŠÖ”
    public void CreateStatus(int Resource,int Fuel)
    {
        ResourceNum = Resource;
        FuelNum = Fuel;

        AddStatus = ResourceNum + FuelNum;
    }
}
