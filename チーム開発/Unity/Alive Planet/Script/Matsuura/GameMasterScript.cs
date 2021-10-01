using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParameter
{
    public float resouce;
    public float fuel;
    public int tarret;
    public int room;
    public int drone;
    public Vector3 pos;
}

public class GameMasterScript : MonoBehaviour
{
    //���̕ϐ��͎g���ĂȂ������������̂ŏ���Ƀf�[�^�Z�[�u�Ɏg�킹�Ă��������!!
    private static ShipParameter s_shipParam;

    public ShipParameter shipParameter
    {
        get
        {
            return s_shipParam;
        }
        set
        {
            s_shipParam = value;
        }
    }

    //�f�[�^�Z�[�u����O�ɂ����Ńf�[�^���i�[���Ă���
    public void SetShip()
    {
        s_shipParam.resouce = ResouceVault;

        s_shipParam.tarret = FuelVault;

        s_shipParam.pos = PlayerPos;
    }

    private static int i_ResouceVault;

    // �����R���e�i
    public int ResouceVault
    {
        get
        {
            return i_ResouceVault;
        }
        set
        {
            i_ResouceVault = value;
        }
    }

    private static int i_FuelVault;

    // �R���R���e�i
    public int FuelVault
    {
        get
        {
            return i_FuelVault;
        }
        set
        {
            i_FuelVault += value;
        }
    }

    // �����h���[�����X�g
    public static List<Drone.DroneData> i_DroneDataList; // �ŏ��ɐ�������h���[��

    public void SetDroneDataList(List<Drone.DroneData> droneDatas)
    {
        i_DroneDataList = droneDatas;
    }

    public List<Drone.DroneData> GetDroneDataList()
    {
        return i_DroneDataList;
    }

    public void ClearDroneDataList()
    {
        i_DroneDataList.Clear();
    }

    private static Vector3 v3_PlayerPos;

    public Vector3 PlayerPos
    {
        get
        {
            return v3_PlayerPos;
        }
        set
        {
            v3_PlayerPos = value;
        }
    }

    //�`���[�g���A����\�����邩?
    private static bool b_Tutorial = true;

    public bool Tutorial
    {
        get
        {
            return b_Tutorial;
        }
        set
        {
            b_Tutorial = value;
        }
    }

    //�T������D�̐���
    private static bool b_CreateShip = true;

    public bool CreateShip
    {
        get
        {
            return b_CreateShip;
        }
        set
        {
            b_CreateShip = value;
        }
    }

    public ShipParameter GetParam()
    {
        return s_shipParam;
    }
}
