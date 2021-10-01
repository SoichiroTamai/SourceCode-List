using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShip : MonoBehaviour
{
    [SerializeField] int fuel;           // �R��
    [SerializeField] int resource;       // ����

    // �I�����Ă���h���[��
    [SerializeField] Drone.DroneData droneData;

    // ��D���������Ă���h���[���̃��X�g
    [SerializeField] List<Drone.DroneData> droneDataList;

    [SerializeField] GameMasterScript gameMaster;

    // �C�x���g�p�ɒl��Ԃ����߂̃N���X
    private Param param = new Param();
    public struct Param
    {
        public int p_resouce;
        public int p_fuel;
    }


    // ��������
    public void ResourceConsume(int consume)
    {
        resource -= consume;
    }

    // �R������
    public void FuelConsume(int consume)
    {
        fuel -= consume;
    }

    // �������
    public void ResourceRecovery(int recovery)
    {
        resource += recovery;
    }

    // �R�����
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
