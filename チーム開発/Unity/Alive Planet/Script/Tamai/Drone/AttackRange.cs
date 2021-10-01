using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;

namespace Drone
{
    public class AttackRange : MonoBehaviour
    {
        private DroneData droneData;

        private CircleCollider2D[] circleCollider;          // 自身の射程距離のコライダー([0]番目)
        private Collider2D　       nearCollider;            // 現在一番近いコライダー
        private List<Collider2D>   withinRangeCollider;     // 現在射程圏内にいるコライダー
        private float              nearColliderDistance;    // コライダーとの距離

        private BulletManager bulletManager; // ヒエラルキー上の弾管理オブジェクト
        private DroneManager  droneManager;  // ヒエラルキー上のドローン管理オブジェクト
        private EnemyManager enemyManager;   // ヒエラルキー上の敵管理オブジェクト
        private EnemyData enemyData;      // 敵のデータ

        void Start()
        {
            bulletManager = GameObject.Find("Bullet Manager").GetComponent<BulletManager>();

            // 射手がドローンの時ドローンデータ取得
            if (this.transform.parent.gameObject.tag == "Drone")
            {
                //// 親オブジェクトからデータを取得
                //droneData = transform.parent.gameObject.GetComponent<DroneData>();

                // ドローンマネージャー取得
                droneManager = GameObject.Find("Drone Manager").GetComponent<DroneManager>();
            }
            else if(this.transform.parent.gameObject.tag == "Enemy")
            {
                // 親オブジェクトからデータを取得
                enemyData = transform.parent.gameObject.GetComponent<EnemyData>();

                // エネミーデータ取得
                enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
            }

            // 射程距離のコライダーを取得
            //circleCollider = this.GetComponents<CircleCollider2D>();
        }

        // トリガー状態のオブジェクトのコライダーと別のオブジェクトのコライダー衝突している間更新する
        private void OnTriggerStay2D(Collider2D other)
        {
            // 攻撃対象が同じ時は更新しない
            //if (other == droneData.attackTarget) { return; }

            //if (other.tag == "MapTile") { return; }
            //if (other.tag == "Bullet") { return; }

            if (other.gameObject.name != "Object_Collider") { return; }

            // 射手と同じタグなら更新しない
            if (other.transform.parent.gameObject.tag == this.transform.parent.gameObject.tag) { return; }
            
            //if (other.gameObject.name == "Attack_Range") { return; }

            // 攻撃対象のタグ一覧
            foreach (var tag in bulletManager.onTriggerTag)
            {
                // 攻撃対象なら
                if (other.transform.parent.gameObject.tag == tag)
                {
                    //Debug.Log("射程圏内に攻撃対象あり");
                    // ドローンが射手
                    if (this.transform.parent.gameObject.tag == "Drone")                 
                    {
                        if (other.gameObject == droneManager.targetObj) { return; }

                        droneManager.targetObj = other.gameObject;                       
                        //droneData.HitorOutRange();
                    }
                    // 敵が射手
                    else if (this.transform.parent.gameObject.tag == "Enemy")
                    {
                        // 攻撃対象をエネミークラスにて設定
                        //if (other.gameObject == enemyManager.targetObj) { return; }
                        enemyData.targetObj = other.gameObject;
                    }
                }
            }
        }

        // コライダーから別のオブジェクトが離れた時
        private void OnTriggerExit2D(Collider2D other)
        {
            // 射手と同じタグなら更新しない
            //if (other.transform.parent.gameObject.tag == this.transform.parent.gameObject.tag) { return; }
            //if (other.tag == "Bullet") { return; }
            //if (other.name == "Attack_Range") { return; }
            if (other.gameObject.name != "Object_Collider") { return; }


            foreach (var tag in bulletManager.onTriggerTag)
            {
                // ドローンが射手
                if (this.transform.parent.gameObject.tag == "Drone")
                {
                    ///Debug.Log("離れた:" + other.name);
                     droneManager.targetObj = null;
                    //droneData.HitorOutRange();
                }
                // 敵が射手
                else if (this.transform.parent.gameObject.tag == "Enemy")
                {
                    // 攻撃対象をエネミークラスにて設定
                    enemyData.targetObj = null;
                }
            }
        }
    }
}