using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drone;
using System.IO;

public class SaveDataJson : MonoBehaviour
{
   //セーブデータ
    [System.Serializable]
    public class SaveData
    {
        // 所持ドローン
        public List<DroneData> DroneSaveDatas;
        // 所持燃料数
        public int Fuel;
        // 所持資源数
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

        // セーブデータが無かったら
        for (int i=1; i < 4; i++)
        {
            if (!File.Exists(datastr + i + ".json"))
            {
                Debug.Log(datastr + i + ".json" + "作成");
                DefaultSave(i);
            }
        }
    }

    // セーブ
    public void Save(int fileNum)
    {
        // セーブする内容をゲームマスターから取得する
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

    // ゲームを始めたときにセーブデータが存在しないときにデフォルトのセーブデータ1を作成
    public void DefaultSave(int fileNum)
    {
        // ドローンデータを1機ずつ生成
        saveData.DroneSaveDatas.Add(attackDroneData);
        saveData.DroneSaveDatas.Add(informationDroneData);
        saveData.DroneSaveDatas.Add(transportDroneData);

        // セーブする内容をゲームマスターから取得する
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

    // ロード
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

            // ロードしたデータで上書き
            saveData = JsonUtility.FromJson<SaveData>(datastr);

            // デバッグ表示
            Debug.Log(saveData.DroneSaveDatas.Count + "機のドローンデータをロードしました");
            Debug.Log(saveData.Fuel + "個の燃料データをロードしました");
            Debug.Log(saveData.Resource + "個の資源データをロードしました");

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

    // ロードしたデータをゲームマスターに送る
    public void SendToGameMaster()
    {
        GameMasterScript gameMaster;

        // ゲームマスターを探してアタッチ
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();

        // セーブデータをゲームマスターにロード
        gameMaster.SetDroneDataList(saveData.DroneSaveDatas);
        gameMaster.FuelVault = saveData.Fuel;
        gameMaster.ResouceVault = saveData.Resource;

        Debug.Log(gameMaster.GetDroneDataList().Count + "個のドローンデータをゲームマスターにロードしました");
        Debug.Log(gameMaster.FuelVault + "個の燃料データをゲームマスターにロードしました");
        Debug.Log(gameMaster.ResouceVault + "個の資源データをゲームマスターにロードしました");
    }

    public void DeleteSaveData(int fileNum)
    {
        string datastr = "";

        datastr = Application.dataPath + "/save" + fileNum + ".json";

        if (File.Exists(datastr))
        {
            File.Delete(datastr);

            Debug.Log("削除");
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
