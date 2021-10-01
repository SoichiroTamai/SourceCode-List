using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drone;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        private BulletManager bulletManager; // ヒエラルキー上の弾管理オブジェクト
        private Rigidbody2D   rb;            // 弾のRigidbody2D

        [SerializeField] private float bulletSpeed = 4f;    // 初速度
        [SerializeField] private float limitSpeed;          //弾の制限速度

        [HideInInspector] public bool       homing;            // ホーミング弾にするか？
        [HideInInspector] public Vector3    direction;         // ドローン全機で現在選択しているターゲットの目標地点
        [HideInInspector] public Vector3    targetDirections;  // 自機からみたターゲットへの方向
        [HideInInspector] public GameObject shooter;  // 射手


        // 個々が記憶するターゲット
        [HideInInspector]
        public GameObject TargetObject;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            bulletManager = GameObject.Find("Bullet Manager").GetComponent<BulletManager>();
        }

        private void FixedUpdate()
        {
            // ターゲット座標の更新 (弾道-追従)
            if (homing)
            {
                // 弾道計算
                targetDirections = direction - transform.position;  // 弾から追いかける対象への方向を計算

                MoveBullet();
            }
            else
            {
                MoveBullet();
            }
        }

        // 弾の移動
        private void MoveBullet()
        {
            rb.AddForce(targetDirections.normalized * bulletSpeed); // 方向の長さを1に正規化、任意の力をAddForceで加える

            float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); // X方向の速度を制限
            float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed); // Y方向の速度を制限

            rb.velocity = new Vector3(speedXTemp, speedYTemp); // 実際に制限した値を代入
        }

        // 当たり判定
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "Object_Collider") { return; }

            foreach (var tag in bulletManager.onTriggerTag)
            {
                if (other.gameObject.tag == tag)
                {
                    // 例外
                    if (other.transform.parent.gameObject.gameObject.tag == shooter.tag) { return; } // 射手
                    if (other.gameObject.name == "Attack_Range") { return; }
                    // 射程距離のオブジェクト
                    //if (other.gameObject.name == "Object_Collider")
                    //{
                    //    //if (shooter != other.transform.parent.gameObject) { return; } // 射手以外のオブジェクトコライダー
                    //    return; //オブジェクトコライダー
                    //}

                    ///Debug.Log("射手：" + shooter);
                    ///Debug.Log(other.name + "に着弾した！");
                    Destroy(this.gameObject);
                }
            }
        }
    }
}