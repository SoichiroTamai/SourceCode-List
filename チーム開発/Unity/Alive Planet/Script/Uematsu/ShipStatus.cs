using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatus : MonoBehaviour
{
    //�����̗�
    public int ResourceNum;
    //�R���̗�
    public int FuelNum;

    //���ނƔR���̍��v��
    private int AddStatus = 0;

    //���ނ̗ʂ��쐬����֐�
    public void CreateStatus(int Resource,int Fuel)
    {
        ResourceNum = Resource;
        FuelNum = Fuel;

        AddStatus = ResourceNum + FuelNum;
    }
}
