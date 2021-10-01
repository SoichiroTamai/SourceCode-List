/*
    -- 変数使用例 --

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
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;
using Pathfinder;

namespace Drone
{
    public class DroneData : CharacterData
    {
        private MultiDatas multiDatas; // 探索シーンでの共有データ

        [Header("ドローンデータ")]
        public DroneType    droneType;     // ドローンタイプ
        private DroneManager droneManager;  // ヒエラルキー上のドローン管理オブジェクト

        [Header("共通データ")]
        public int droneRes;  // ドローンが持っている資源(HP)
        [SerializeField] bool isSceneRepair = false;
        private CharacterData charaData;
        private ResouceScript resouceManager;

        [Header("固有データ")]
        // 運搬
        public int getResourceNum; // 取得したリソースの数


        [Header("経路探索")]
        [SerializeField] private float moveSpeed = 0.0005f;
        private Vector3 startPos;
        private bool moveFlg;           // 移動中か     true = 移動中 
        private bool moveFlg_EndReport; // 移動完了通知 true = 移動完了

        //private bool Key_flg;
        private int moveFrame;
        private float present_Location_sum;
        private float distance_two;

        private List<Vector2> move_Path;   // スタートからゴールまでの経路

        public List<GameObject> target_Resources;  // 回収する資源

        // 複数回感知されるのを防止用
        List<GameObject> resourcesRecovered = new List<GameObject>(); // 収集した資源

        // Start is called before the first frame update
        void Start()
        {
            // 修理シーンなら
            if (isSceneRepair) { return; }

            // Init();
        }

        public void Init()
        {
            // 共有データを取得
            //multiDatas = GameObject.Find(new MultiDatas().findName).GetComponent<MultiDatas>();
            multiDatas = GameObject.Find("Multi Datas").GetComponent<MultiDatas>();

            // ドローンマネージャー取得
            droneManager = GameObject.Find("Drone Manager").GetComponent<DroneManager>();

            // 探索中以外なら実行しない
            if (multiDatas.searchStatus != SearchStatus.search) { return; }

            // ドローンタイプ
            if (this.droneType == DroneType.none)
            {
                Debug.LogWarning(this.name + "：ドローンのデータタイプを指定し忘れていませんか？");
            }

            charaData = GameObject.Find("CharacterData").GetComponent<CharacterData>();
            resouceManager = GameObject.Find("ResouceManager").GetComponent<ResouceScript>();

            move_Path = new List<Vector2>();

            //startPos = new Vector3();
            moveFlg = false;
            moveFlg_EndReport = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(multiDatas == null) { return; }

            // 探索中以外なら実行しない
            if (multiDatas.searchStatus != SearchStatus.search) { return; }

            // ドローンのタイプ別に更新
            switch (droneType)
            {
                // 運搬型
                case DroneType.transport:
                    Update_Transport();
                    break;

                // 攻撃型
                case DroneType.attack:
                    Update_Attack();
                    break;

                // 情報型
                case DroneType.information:
                    Update_Information();
                    break;
            }

            //Path_Move();
        }

        // 運搬型の更新処理
        void Update_Transport()
        {
            //transform.Translate(0.001f, 0f, 0f);  // (確認用)

            if (target_Resources.Count <= 0) { return; }

            for (int i = target_Resources.Count-1; i >= 0; i--)
            {
                if (!target_Resources[i])
                {
                    target_Resources.RemoveAt(i);
                }
            }

            int t_ResC = target_Resources.Count - 1;

            // 移動完了後なら
            if (moveFlg_EndReport)
            {
                moveFlg_EndReport = false;
                moveFlg = false;

                for (int i = 0; i < target_Resources.Count; i++)
                {
                    // 回収したリソースを回収予定一覧から削除
                    if (!target_Resources[i])
                    {
                        target_Resources.RemoveAt(i); // リストのi番目を削除する ※ OnTriggerEnter2D にて 事前にDestroy()を実行済み
                    }
                }

                return;
            }

            if (target_Resources[t_ResC] == null) { return; }

            // リソースがあれば回収
            if (!moveFlg)
            {
                moveFlg = true;
                moveFlg_EndReport = false;
                moveFrame = 0;
                //Pathfinding pathfinding = new Pathfinding();
                //pathfinding = this.gameObject.GetComponent<Pathfinding>();
                move_Path = this.gameObject.GetComponent<Pathfinding>().Get_Pathfinding(this.transform.position, target_Resources[t_ResC].transform.position);
                startPos = this.transform.position;
                distance_two = Vector2.Distance(startPos, move_Path[0]);
                present_Location_sum = 0.0f;
            }

            Path_Move();
        }

        // 攻撃型の更新処理
        void Update_Attack()
        {
            // 修理シーンなら
            if (isSceneRepair) { return; }
            charaData.UpdateShot(droneManager.targetObj, this.gameObject);
        }

        // 情報型の更新処理
        void Update_Information()
        {
            // 情報型
        }

        // 当たり判定
        private void OnTriggerEnter2D(Collider2D other)
        {
            // 耐久値減算等の処理追加予定地 ----------------------

            // -------------------------------------------------

            // ↓運搬型ドローンのみ使用
            if (droneType != DroneType.transport) { return; }

            // Resourceの時
            if (other.tag == "Resource")
            {
                if (!resourcesRecovered.Contains(other.gameObject))
                {
                    Destroy(other.gameObject);
                    getResourceNum++;
                    resouceManager.AddResourceRecoveredSum();
                    resourcesRecovered.Add(other.gameObject);

                    moveFlg_EndReport = true;
                }
            }
        }

        // コライダーが離れた時
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Resource")
            {
                resetResourcesRecoveredList();
            }
        }

        // 収集した資源を破棄
        private void resetResourcesRecoveredList()
        {
            resourcesRecovered.Clear();
        }

        // 指定したパス(move_Path)へ移動
        void Path_Move()
        {
            // 移動パスがあれば移動
            if (move_Path.Count <= 0) { return; }

            // 現在の位置
            float present_Location = (Time.time * moveSpeed) / distance_two;

            transform.position = Vector3.Lerp(startPos, move_Path[moveFrame], present_Location_sum);

            present_Location_sum += present_Location;

            // 保管なしの移動
            //this.transform.position = move_Path[moveFrame];

            // moveFrame の 場所まで移動が完了した
            if (present_Location_sum >= 1.0f)
            {
                present_Location_sum = 0.0f;

                // 最後のタイルパス(ゴール地点)なら終了
                if (moveFrame >= move_Path.Count - 1)
                {
                    moveFrame = 0;
                    move_Path.Clear();
                    moveFlg_EndReport = true;
                    return;
                }

                startPos = move_Path[moveFrame];
                distance_two = Vector2.Distance(startPos, move_Path[moveFrame + 1]);
                moveFrame++;
            }

        }
    }
}