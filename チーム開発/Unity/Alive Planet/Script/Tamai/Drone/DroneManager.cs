/*
    -- 変数使用例 --

    // ドローン全体
    foreach (var d in droneDatas)
    {
        switch (d.droneType)
        {
            // 運搬型のみ
           case DroneType.transport:
                d.transform.Translate(0.001f, 0f, 0f);
                break;

           // 攻撃型のみ
           case DroneType.attack:
                d.transform.Rotate(0f, 0f, 0.1f);
                break;

           // 情報型のみ
           case DroneType.information:
                d.transform.Translate(0.0f, 0.001f, 0f);
                break;
         }
    }
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal; //Light2Dを使うのに必要

namespace Drone
{
    // ドローンタイプ
    public enum DroneType
    {
        none,       // 無し
        transport,  // 運搬
        attack,     // 攻撃
        information // 艦内の情報を取得出来る (マップ,ハッキング)
    }

    // ドローンの全体のデータ管理
    public class DroneManager : MonoBehaviour
    {
        [Header("Multi Datas")]
        [SerializeField] private MultiDatas multiDatas; // 探索シーンでの共有データ

        // ライト
        [Header("ライト")]
        private float pointLlght_AddOuterRadius; // ポイントライトの範囲の加算量
        [SerializeField] private float droneLite_one_OuterRadius = 1.0f; // 情報型ドローン１体当たりのライトの拡大サイズ

        // ドローン
        [Header("ドローンの生成方法 ※説明文あり")]
        [SerializeField, TooltipAttribute("true  = 各機体数を指定し生成\nfalse = 配列での生成(保持しているドローンの等)")]
        private bool generate_a_SpecifiedNumber;

        [Header("生成時のオブジェクトデータ")]
        [SerializeField] private GameObject drones;   // ヒエラルキー上に居るドローン全機

        [SerializeField] public GameObject DroneObject_Attack;      // 攻撃型ドローンのオブジェクトデータ
        [SerializeField] public GameObject DroneObject_Information; // 情報型ドローンのオブジェクトデータ
        [SerializeField] public GameObject DroneObject_Transport;   // 運搬型ドローンのオブジェクトデータ

        [Header("ドローン情報")]
        [HideInInspector] public List<DroneData> droneDatas;   // ドローン情報格納用
        [HideInInspector] public GameObject targetObj; // ドローンの攻撃対象

        [Header("ドローンの生成数 (generate_a_SpecifiedNumber = true の時)")]
        [SerializeField] private int AttackDrone_Num;        // 最初に生成する攻撃型ドローンの数
        [SerializeField] private int TransportDrone_Num;     // 最初に生成する運搬型ドローンの数
        [SerializeField] private int InformationDrone_Num;   // 最初に生成する情報型ドローンの数

        [Header("生成するドローン (generate_a_SpecifiedNumber = false の時)")]
        public GameObject[] instantiateDroneObject; // 最初に生成するドローン

        // ドローンの移動量
        private Vector3 oldPos = new Vector3();

        // 各タイプの個体数
        private Vector3Int by_type_num; // x…攻撃, y…運搬, z…情報

        // 情報型
        private bool flg_getDownKey;
        private bool flg_wideRange;      // ライト広域化を実行したか (true=広域化)
        private bool flg_wideRange_now;  // ライト広域化中か (true=広域化)
        private bool flg_coolTime;       // 広域化使用後か

        [Header("ドローン固有データ -情報型-")]
        [SerializeField] private float wideRange_effectTime_Max;       // 広域化の効果時間
        private float wideRange_effectTime_remaining; // 広域化の効果残り時間
        [SerializeField] private float wideRange_coolTime_Max;         // 広域化が再使用出来るまでの時間
        private float wideRange_coolTime;             // 広域化が再使用出来るまでの経過時間


        // 指定したドローンを指定数生成する
        public void InstanceDrone(GameObject _droneObject, Vector3 pos = new Vector3(), int _instanceNum = 1, string _droneObjectName = "Object")
        {
            // ドローン生成
            Quaternion quaternion = new Quaternion();

            if (_droneObject)
            {
                for (int num = 0; num < _instanceNum; num++)
                {
                    Instantiate(_droneObject, pos, quaternion, drones.transform);
                }
            }
            else
            {
                Debug.LogWarning(_droneObjectName + " が設定されていません");
            }
        }

        // タイプを指定して生成
        public void InstantiateDrone(DroneType droneType, Vector3 pos)
        {
            GameObject droneObject = null;
            Quaternion quaternion = new Quaternion();

            switch (droneType)
            {
                case Drone.DroneType.attack:
                    droneObject = DroneObject_Attack;
                    break;

                case Drone.DroneType.information:
                    droneObject = DroneObject_Information;
                    break;

                case Drone.DroneType.transport:
                    droneObject = DroneObject_Transport;
                    break;
            }

            if (droneObject)
            {
                Instantiate(droneObject, pos, quaternion, drones.transform);
            }
        }

        // Start is called before the first frame update
        public void Start()
        {
            multiDatas.droneSelect.dropDownDatas.options[0].text = "Free Camera";
            Search_Start();
        }

        void Search_Start()
        {
            if (multiDatas.searchStatus != SearchStatus.search) { return; }

            // フラグ初期化
            flg_getDownKey = false;
            flg_wideRange = false;

            // 各個体数を初期化
            by_type_num = new Vector3Int();

            /*
            Vector3 pos = new Vector3();
            Quaternion quaternion = new Quaternion();

            // 各機体数を指定し生成
            if (generate_a_SpecifiedNumber)
            {
                Debug.Log("各機体数を指定し生成");

                InstanceDrone(DroneObject_Attack, _instanceNum: AttackDrone_Num, _droneObjectName: nameof(DroneObject_Attack)); // 攻撃型ドローン　※ nameof() 変数名を取得
                InstanceDrone(DroneObject_Transport, _instanceNum: TransportDrone_Num, _droneObjectName: nameof(DroneObject_Transport));         // 運搬型ドローン
                InstanceDrone(DroneObject_Information, _instanceNum: InformationDrone_Num, _droneObjectName: nameof(DroneObject_Information)); // 情報型ドローン

            }
            // 配列での生成
            else
            {
                Debug.Log("配列指定での生成");

                foreach (var drone in instantiateDroneObject)
                {
                    Instantiate(drone, pos, quaternion, drones.transform);
                }
            }
            */

            //foreach(var data in multiDatas.)
            //InstantiateDrone();

            // ポイントライトの加算分を初期化
            pointLlght_AddOuterRadius = 0.0f;

            // 最初に設定されているドローンの情報を格納する
            for (int i = 0; i < drones.transform.childCount; i++)
            {
                // オブジェクトデータ取得
                DroneData droneDataScript = drones.transform.GetChild(i).gameObject.GetComponent<DroneData>();

                droneDataScript.Init();

                // ドローンリストに追加
                droneDatas.Add(droneDataScript);

                // 各タイプごとの個体を格納及び共通項目の設定
                if (droneDataScript.droneType == DroneType.attack)
                {
                    by_type_num.x++;
                }
                else if (droneDataScript.droneType == DroneType.transport)
                {
                    by_type_num.y++;
                }
                else if (droneDataScript.droneType == DroneType.information)
                {
                    by_type_num.z++;
                    pointLlght_AddOuterRadius += droneLite_one_OuterRadius; // ライトの範囲を拡大(1台辺り 1.0f拡大)
                }
            }


            // 追従するオブジェクトが指定されていなければ 1番目のドローン に設定
            if (multiDatas.virtualCamera.Follow == null)
            {
                multiDatas.cameraFollowObject = droneDatas[0].gameObject;        // 追従するオブジェクトの設定
                multiDatas.virtualCamera.Follow = multiDatas.cameraFollowObject.transform;  // 情報を反映
                Debug.LogWarning("追従するオブジェクトが指定されていなかった為、" + multiDatas.cameraFollowObject.name + " を設定しました。");
            }

            // グローバルライト
            //llght2DIntensity = 0.0f;
            //globalLight2DObject.intensity = llght2DIntensity;

            // ポイントライト
            multiDatas.pointLight2DObject.pointLightOuterRadius += pointLlght_AddOuterRadius;

            // 時間初期化
            wideRange_effectTime_Max = by_type_num.z + 2.0f;
            wideRange_effectTime_remaining = wideRange_effectTime_Max;

            wideRange_coolTime_Max = 5.0f;
            wideRange_coolTime = 0.0f;

            // 回収予定のリソースを割り当てる
            AllocateResources();
        }

        // Update is called once per frame
        void Update()
        {
            if (multiDatas.searchStatus != SearchStatus.search) { return; }

            // タイプ別に実行する能力
            Update_Information();
        }

        // 情報型の更新処理
        void Update_Information()
        {
            // 情報型がいなければ更新しない
            if (by_type_num.z <= 0) { return; }

            if (Input.GetKey(KeyCode.Space))
            {
                if (!flg_getDownKey && !flg_coolTime)
                {
                    flg_wideRange = true;
                    flg_getDownKey = true;
                }
            }
            else
            {
                flg_getDownKey = false;
            }

            wideRange();
        }

        // 広域化
        void wideRange()
        {
            if (flg_wideRange)
            {
                multiDatas.pointLight2DObject.pointLightOuterRadius += pointLlght_AddOuterRadius + 1.0f;
                flg_wideRange_now = true;
                flg_coolTime = true;
            }

            // 広域化後
            if (flg_wideRange_now)
            {
                flg_wideRange = false;
                wideRange_effectTime_remaining += Time.deltaTime;

                // 広域化解除
                if (wideRange_effectTime_remaining >= wideRange_effectTime_Max)
                {
                    wideRange_effectTime_remaining = 0.0f;
                    flg_wideRange_now = false;
                    multiDatas.pointLight2DObject.pointLightOuterRadius -= pointLlght_AddOuterRadius + 1.0f;
                }
            }

            // クールタイム
            if (!flg_wideRange_now && flg_coolTime)
            {
                wideRange_coolTime += Time.deltaTime;
                if (wideRange_coolTime >= wideRange_coolTime_Max)
                {
                    wideRange_coolTime = 0.0f;
                    flg_coolTime = false;
                }
            }
        }

        // 回収するリソースの自動割り当て（担当）
        void AllocateResources()
        {
            // 運搬型がいなければ更新しない
            if (by_type_num.y <= 0) { return; }

            // 回収予定のリソースが無ければ更新しない
            if (multiDatas.target_Resources.Count <= 0) { return; }

            //　各機体の回収予定数個数
            int fraction = multiDatas.target_Resources.Count % by_type_num.y;           // 端数   … 一部のドローンが分配して1ずつ負担
            int num = (multiDatas.target_Resources.Count - fraction) / by_type_num.y;   // 回収数 … 全機体が担当する最低個数

            foreach (var drone in droneDatas)
            {
                if (drone.droneType == DroneType.transport)
                {
                    for(int i = 0; i < num; i++)
                    {
                        // 割り当て終了
                        if (multiDatas.target_Resources.Count <= 0) { return; }
                           
                        drone.target_Resources.Add(multiDatas.target_Resources[multiDatas.target_Resources.Count - 1]);
                        multiDatas.target_Resources.RemoveAt(multiDatas.target_Resources.Count - 1);
                    }

                    if(fraction > 0)
                    {
                        // 割り当て終了
                        if (multiDatas.target_Resources.Count <= 0) { return; }

                        drone.target_Resources.Add(multiDatas.target_Resources[multiDatas.target_Resources.Count - 1]);
                        multiDatas.target_Resources.RemoveAt(multiDatas.target_Resources.Count - 1);
                        fraction--;
                    }
                }
            }


            //while(multiDatas.target_Resources.Count > 0)
            //{
            //    foreach (var drone in droneDatas)
            //    {
            //        if (drone.droneType == DroneType.transport)
            //        {
            //            if (multiDatas.target_Resources.Count <= 0) { continue; }

            //            drone.target_Resources.Add(multiDatas.target_Resources[multiDatas.target_Resources.Count - 1]);
            //            multiDatas.target_Resources.RemoveAt(multiDatas.target_Resources.Count - 1);
            //        }
            //    }
            //}

        }
    }
}