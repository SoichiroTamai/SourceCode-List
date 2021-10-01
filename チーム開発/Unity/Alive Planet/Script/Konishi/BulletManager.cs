using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public class BulletManager : MonoBehaviour
    {
        private GameObject bullets;          // ヒエラルキー上のオブジェクト(親) 
        public  GameObject bulletObject;     // 弾のプレハブ化されたオブジェクト
        public  float      coolTime = 0.5f;  // クールタイム
        private float      currentTime = 0f; // 最後に弾を撃ってからの経過時間
        public  string[]   onTriggerTag;        // 弾の当たり判定を行うタグ

        private void Awake()
        {
            // 弾の親オブジェクトの生成
            Debug.Log("bullets生成");
            bullets = new GameObject("Bullets");
        }

        // 発砲 (射手から攻撃対象のオブジェクト,射手)
        public void Shot(GameObject targetObj,GameObject shooter)
        {
             if (targetObj == null) { return; }

            foreach (var tag in onTriggerTag)
            {
                // 攻撃対象だったら
                if (targetObj.transform.parent.gameObject.tag == tag)
                {
                    // 経過時間加算
                    currentTime += Time.deltaTime;

                    // クールタイムが終わったら
                    if (currentTime > coolTime)
                    {
                        //Debug.LogFormat("{0}秒経過", span);
                        currentTime = 0f;

                        /// Debug.Log("弾-生成");
                        Instantiate(bulletObject, shooter.transform.position, shooter.transform.rotation, bullets.transform);

                        // 生成した弾のデータを取得
                        Bullet bulletData = bullets.transform.GetChild(bullets.transform.childCount - 1).gameObject.GetComponent<Bullet>();

                        // 射手を設定
                        bulletData.shooter = shooter;

                        // 攻撃対象を設定
                        bulletData.TargetObject = targetObj;

                        // 目標地点
                        bulletData.direction = targetObj.transform.position;

                        // 弾から追いかける対象への方向を計算
                        bulletData.targetDirections = bulletData.direction - shooter.transform.position;
                    }
                }
            }
        }

        // 射程圏内に攻撃対象が入った時及び出た時
        public void HitorOutRange(GameObject parentObj, GameObject childObj)
        {
            // 射程圏内の攻撃対象を設定
            parentObj = childObj;
        }
    }
}
