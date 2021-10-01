using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //データを保存しているファイルのパス名
    string filePath;
    //保存するデータを保有しているスクリプト
    GameMasterScript gameMaster;

    private void Awake()
    {
        //ファイルのパスの場所を設定しGameMasterを作成する
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        gameMaster = new GameMasterScript();
    }

    public void Save()
    {
        //データをセットしてJsonで使えるように変換
        gameMaster.SetShip();
        string json = JsonUtility.ToJson(gameMaster.shipParameter);
        //ストリームを使ってデータを書き込む
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if(File.Exists(filePath))
        {
            //ストリームを使ってデータを書き込む
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();

            //書き込んだデータをゲームマスターに代入する
            gameMaster.shipParameter = JsonUtility.FromJson<ShipParameter>(data);
        }
    }
}
