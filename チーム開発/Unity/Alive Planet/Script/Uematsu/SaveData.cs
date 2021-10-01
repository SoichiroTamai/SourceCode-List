using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : Object
{
    //public�f�[�^
    public int intData;
    //static�f�[�^
    static int staticData;
    //private�f�[�^
    private float floatData;
    //bool�f�[�^
    public bool boolData;

    //�V���A���C�Y�ȃf�[�^
    [SerializeField]
    private Vector2 vector;
    [SerializeField]
    private GameObject gameObject;

    public string dataText;

    //�f�[�^���Z�b�g����֐��Q
    public void Setint(int intdata)
    {
        intData = intdata;
    }

    public void SetStatic(int staticdata)
    {
        staticData = staticdata;
    }

   public void Setbool(bool flag)
    {
        this.boolData = flag;
    }

    public void SetVector(Vector2 Vector)
    {
        this.vector = Vector;
    }

    public void SetObject(GameObject obj)
    {
        this.gameObject = obj;
    }

    //�f�[�^���Q�b�g����֐��Q

    public int Getint()
    {
        return intData;
    }

    public int GetStatic()
    {
        return staticData;
    }

    public bool Getbool()
    {
        return boolData;
    }

    public Vector2 SetVector()
    {
        return vector;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    public string GetNormalData()
    {
        return "int: " + intData + " static: " + staticData + " bool: " + boolData + " vector: " + vector + " obj: " + gameObject;
    }

    public string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }
}
