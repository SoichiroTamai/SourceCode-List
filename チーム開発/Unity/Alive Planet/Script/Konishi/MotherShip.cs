using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShip : MonoBehaviour
{
    [SerializeField] int fuel;           // 燃料
    [SerializeField] int resource;       // 資源

    // 選択しているドローン
    [SerializeField] Drone.DroneData droneData;

    // 母船が所持しているドローンのリスト
    [SerializeField] List<Drone.DroneData> droneDataList;

    [SerializeField] GameMasterScript gameMaster;

    // イベント用に値を返すためのクラス
    private Param param = new Param();
    public struct Param
    {
        public int p_resouce;
        public int p_fuel;
    }


    // 資源消費
    public void ResourceConsume(int consume)
    {
        resource -= consume;
    }

    // 燃料消費
    public void FuelConsume(int consume)
    {
        fuel -= consume;
    }

    // 資源回収
    public void ResourceRecovery(int recovery)
    {
        resource += recovery;
    }

    // 燃料回収
    public void FuelRecovery(int recovery)
    {
        fuel += recovery;
    }

    public int GetFuel() { return fuel; }

    public int GetResource() { return resource; }

    public void SetDroneDataToGameMaster()
    {
        //gameMaster.ClearDroneDataList();
        //droneData = GameObject.Find("DroneTransport").GetComponent<Drone.DroneData>();
        //droneDataList.Add(droneData);
        //droneData = GameObject.Find("DroneAttack").GetComponent<Drone.DroneData>();
        //droneDataList.Add(droneData);
        //droneData = GameObject.Find("DroneSearch").GetComponent<Drone.DroneData>();
        //droneDataList.Add(droneData);
        //gameMaster.SetDroneDataList(droneDataList);
        //Debug.Log(droneDataList);
    }

    public Param GetResouceParam()
    {
        return param;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameMaster.FuelVault = 10;
        gameMaster.ResouceVault = 10;
        fuel = gameMaster.FuelVault;
        resource = gameMaster.ResouceVault;
        droneDataList = gameMaster.GetDroneDataList();
    }

    // Update is called once per frame
    void Update()
    {
        param.p_resouce = resource;
        param.p_fuel = fuel;
    }
}
