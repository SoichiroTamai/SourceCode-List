using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drone;
using System.IO;

public class SaveDataJson : MonoBehaviour
{
   //�Z�[�u�f�[�^
    [System.Serializable]
    public class SaveData
    {
        // �����h���[��
        public List<DroneData> DroneSaveDatas;
        // �����R����
        public int Fuel;
        // ����������
        public int Resource;
    }

    SaveData saveData = new SaveData();

    public GameMasterScript gameMasterScript;

    [SerializeField]
    private DroneData attackDroneData;

    [SerializeField]
    private DroneData transportDroneData;

    [SerializeField]
    private DroneData informationDroneData;

    private void Start()
    {
        string datastr = "";

        saveData.DroneSaveDatas = new List<DroneData>();

        datastr = Application.dataPath + "/save";

        // �Z�[�u�f�[�^������������
        for (int i=1; i < 4; i++)
        {
            if (!File.Exists(datastr + i + ".json"))
            {
                Debug.Log(datastr + i + ".json" + "�쐬");
                DefaultSave(i);
            }
        }
    }

    // �Z�[�u
    public void Save(int fileNum)
    {
        // �Z�[�u������e���Q�[���}�X�^�[����擾����
        saveData.DroneSaveDatas.Clear();
        foreach(var data in gameMasterScript.GetDroneDataList())
        {
            saveData.DroneSaveDatas.Add(data);
        }
        saveData.Fuel = gameMasterScript.FuelVault;
        saveData.Resource = gameMasterScript.ResouceVault;

        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save" + fileNum + ".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    // �Q�[�����n�߂��Ƃ��ɃZ�[�u�f�[�^�����݂��Ȃ��Ƃ��Ƀf�t�H���g�̃Z�[�u�f�[�^1���쐬
    public void DefaultSave(int fileNum)
    {
        // �h���[���f�[�^��1�@������
        saveData.DroneSaveDatas.Add(attackDroneData);
        saveData.DroneSaveDatas.Add(informationDroneData);
        saveData.DroneSaveDatas.Add(transportDroneData);

        // �Z�[�u������e���Q�[���}�X�^�[����擾����
        saveData.Fuel = 30;
        saveData.Resource = 10;

        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save"+ fileNum +".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public void SaveGameData1()
    {
        Save(1);
    }
    public void SaveGameData2()
    {
        Save(2);
    }
    public void SaveGameData3()
    {
        Save(3);
    }

    // ���[�h
    public void Load(int fileNum)
    {
        string datastr = "";
        StreamReader reader;

        datastr = Application.dataPath + "/save" + fileNum + ".json";

        if (File.Exists(datastr))
        {
            reader = new StreamReader(datastr);

            datastr = reader.ReadToEnd();
            reader.Close();

            // ���[�h�����f�[�^�ŏ㏑��
            saveData = JsonUtility.FromJson<SaveData>(datastr);

            // �f�o�b�O�\��
            Debug.Log(saveData.DroneSaveDatas.Count + "�@�̃h���[���f�[�^�����[�h���܂���");
            Debug.Log(saveData.Fuel + "�̔R���f�[�^�����[�h���܂���");
            Debug.Log(saveData.Resource + "�̎����f�[�^�����[�h���܂���");

            SendToGameMaster();
        }
    }

    public void LoadGameData1()
    {
        Load(1);
    }
    public void LoadGameData2()
    {
        Load(2);
    }
    public void LoadGameData3()
    {
        Load(3);
    }

    // ���[�h�����f�[�^���Q�[���}�X�^�[�ɑ���
    public void SendToGameMaster()
    {
        GameMasterScript gameMaster;

        // �Q�[���}�X�^�[��T���ăA�^�b�`
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();

        // �Z�[�u�f�[�^���Q�[���}�X�^�[�Ƀ��[�h
        gameMaster.SetDroneDataList(saveData.DroneSaveDatas);
        gameMaster.FuelVault = saveData.Fuel;
        gameMaster.ResouceVault = saveData.Resource;

        Debug.Log(gameMaster.GetDroneDataList().Count + "�̃h���[���f�[�^���Q�[���}�X�^�[�Ƀ��[�h���܂���");
        Debug.Log(gameMaster.FuelVault + "�̔R���f�[�^���Q�[���}�X�^�[�Ƀ��[�h���܂���");
        Debug.Log(gameMaster.ResouceVault + "�̎����f�[�^���Q�[���}�X�^�[�Ƀ��[�h���܂���");
    }

    public void DeleteSaveData(int fileNum)
    {
        string datastr = "";

        datastr = Application.dataPath + "/save" + fileNum + ".json";

        if (File.Exists(datastr))
        {
            File.Delete(datastr);

            Debug.Log("�폜");
        }
    }

    public void DeleteGameData1()
    {
        DeleteSaveData(1);
    }
    public void DeleteGameData2()
    {
        DeleteSaveData(2);
    }
    public void DeleteGameData3()
    {
        DeleteSaveData(3);
    }
}
