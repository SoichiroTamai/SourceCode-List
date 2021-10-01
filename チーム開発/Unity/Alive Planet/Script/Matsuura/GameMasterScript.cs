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
    //この変数は使ってなさそうだったので勝手にデータセーブに使わせてもらったぜ!!
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

    //データセーブする前にここでデータを格納しておく
    public void SetShip()
    {
        s_shipParam.resouce = ResouceVault;

        s_shipParam.tarret = FuelVault;

        s_shipParam.pos = PlayerPos;
    }

    private static int i_ResouceVault;

    // 資源コンテナ
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

    // 燃料コンテナ
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

    // 所持ドローンリスト
    public static List<Drone.DroneData> i_DroneDataList; // 最初に生成するドローン

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

    //チュートリアルを表示するか?
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

    //探索する船の生成
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
