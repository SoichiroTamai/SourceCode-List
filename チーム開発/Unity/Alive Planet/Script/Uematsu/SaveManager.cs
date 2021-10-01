using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //�f�[�^��ۑ����Ă���t�@�C���̃p�X��
    string filePath;
    //�ۑ�����f�[�^��ۗL���Ă���X�N���v�g
    GameMasterScript gameMaster;

    private void Awake()
    {
        //�t�@�C���̃p�X�̏ꏊ��ݒ肵GameMaster���쐬����
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        gameMaster = new GameMasterScript();
    }

    public void Save()
    {
        //�f�[�^���Z�b�g����Json�Ŏg����悤�ɕϊ�
        gameMaster.SetShip();
        string json = JsonUtility.ToJson(gameMaster.shipParameter);
        //�X�g���[�����g���ăf�[�^����������
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if(File.Exists(filePath))
        {
            //�X�g���[�����g���ăf�[�^����������
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();

            //�������񂾃f�[�^���Q�[���}�X�^�[�ɑ������
            gameMaster.shipParameter = JsonUtility.FromJson<ShipParameter>(data);
        }
    }
}
