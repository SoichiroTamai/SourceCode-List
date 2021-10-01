using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{
    private enum StatusNum
    {
        Lack,//少ない
        middle,//中間
        Excess,//多い
    }

    //宇宙船の最小値と最大値
    public int minShip = 2;
    public int maxShip = 5;

    //配置する最大幅高さと最小幅高さ
    public float minStageX = -25;
    public float maxStageX = 25;
    public float minStageY = 0;
    public float maxStageY = 25;
    public int HierarchyNum = 5;

    //Yの値の最大値
    private float MaxPointY = 0;

    //惑星と宇宙船のプレハブ
    public ShipStatus MinShipObject;
    public ShipStatus MedShipObject;
    public ShipStatus ManyShipObject;
    public PlanetStatus PlanetObject;

    //宇宙船の資材の最低値と最大値
    public int minStatus = 1;
    public int medStatus = 5;
    public int maxStatus = 10;

    //宇宙船一つで資材が多いか少ないか基準の数
    public int minOneStatus = 5;
    public int manyOneStatus = 7;

    //救済を使うかどうか
    public int ReliefMinNum = 30;//少ない数
    public int ReliefManyNum = 60;//多い数
    private int AddStatus = 0;
    private bool ReliefFlg = false;

    private StatusNum RelieStatus = StatusNum.middle;

    //プレハブを子オブジェクトにするためのオブジェクト
    private ShipStatus Obj;
    public GameObject Parent;

    //生成するかどうかの変数を管理してくれているゲームマスター君
    public GameMasterScript masterScript;

    // Start is called before the first frame update
    void Start()
    {
         for (int i = 0; i < HierarchyNum; i++)
         {
            CreateShip(i);
         }

         CreatePlanet();

         masterScript.CreateShip = false;
    }

    //宇宙船の生成
    void CreateShip(int Hierarchy)
    {
        //宇宙船の生成を決定する
        int ShipCount = Random.Range(minShip, maxShip);

        //決定した宇宙船の数分宇宙船を生成
        for (int i = 0; i < ShipCount; i++)
        {
            float PointX = Random.Range(minStageX, maxStageX);
            float PointY = Random.Range(minStageY, maxStageY) + (maxStageY * Hierarchy);

            //目的地設定のためY軸の最大値を取っておく
            if(PointY>MaxPointY)
            {
                MaxPointY = PointY;
            }

            int Resource = 0;
            int Fuel = 0;

            //リソースの量を作成しその数を合計しておく
            if (RelieStatus==StatusNum.Lack)//前の列が少なければ
            {
                Resource = Random.Range(medStatus, maxStatus);
                Fuel = Random.Range(medStatus, maxStatus);
            }
            else if(RelieStatus==StatusNum.Excess)//前の列が多い場合
            {
                Resource = Random.Range(minStatus, medStatus);
                Fuel = Random.Range(minStatus, medStatus);
            }
            else//それ以外の場合は最大値から最小値までで回す
            {
                Resource = Random.Range(minStatus, maxStatus);
                Fuel = Random.Range(minStatus, maxStatus);
            }

            AddStatus += Resource + Fuel;

            //リソースの量と共にオブジェクトを作成
            if ((Resource + Fuel) < minOneStatus)
            {
                MinShipObject.CreateStatus(Resource, Fuel);
                Obj=Instantiate(MinShipObject, new Vector3(PointX, PointY, 0), Quaternion.identity);
                Obj.transform.parent = Parent.transform;
            }
            else if ((Resource + Fuel) > manyOneStatus)
            {
                ManyShipObject.CreateStatus(Resource, Fuel);
                Obj = Instantiate(ManyShipObject, new Vector3(PointX, PointY, 0), Quaternion.identity);
                Obj.transform.parent = Parent.transform;
            }
            else
            {
                MedShipObject.CreateStatus(Resource, Fuel);
                Obj = Instantiate(MedShipObject, new Vector3(PointX, PointY, 0), Quaternion.identity);
                Obj.transform.parent = Parent.transform;
            }
        }

        //救済を使う基準よりも低かったら救済措置を実行
        if(ReliefMinNum>AddStatus)
        {
            RelieStatus = StatusNum.Lack;
        }
        else if(ReliefManyNum<AddStatus)//多ければ少なくなるように設定
        {
            RelieStatus = StatusNum.Excess;
        }
        else//それ以外は普通に
        {
            RelieStatus = StatusNum.middle;
        }

        Debug.Log(RelieStatus.ToString());
        Debug.Log(AddStatus);

        //合計地のリセット
        AddStatus = 0;
    }

    void CreatePlanet()
    {
        //目的地の惑星の座標を作り生成する
        float PointX = Random.Range(minStageX, maxStageX);
        float PointY = Random.Range(minStageY, maxStageY);

        Instantiate(PlanetObject, new Vector3(PointX, PointY+MaxPointY, 0), Quaternion.identity);
    }
}
