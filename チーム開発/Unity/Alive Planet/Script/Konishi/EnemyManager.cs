using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemys;      // 生成したエネミーを格納する場所 (ヒエラルキー)
    public GameObject enemyObject; // 生成するエネミーのオブジェクト
    public MapCreate mapCreate;

    [SerializeField] private List<EnemyData> enemyList;
    [SerializeField] public GameObject targetObj;

    // 生成する敵の数
    public int spawnEnemyNum;

    private int mapBloackList_OldCount;

    // Start is called before the first frame update
    void Start()
    {
        mapBloackList_OldCount = 0;

        // 生成した敵の格納場所を作成
        enemys = new GameObject("Enemys");
    }

    void Update()
    {
        if (mapCreate.tellMapCreate)
        {
            ///Debug.Log("敵生成開始");

            ///Debug.Log("OldCount：" + mapBloackList_OldCount);
            ///Debug.Log("Count：" + (mapCreate.MapBloackList.Count - 1));

            // 順不同な乱数を取得(生成場所)
            var ary = uniqRandAry(2, mapCreate.MapBlockList.Count - 1, spawnEnemyNum);

            ///Debug.Log("ary.Length：" + ary.Length);

            for (int s = 0; s < ary.Length; s++)
            {
                ///Debug.Log("生成した乱数：" + ary[s]);
                SpawnEnemy(mapCreate.MapBlockList[ary[s]].transform.position);
            }

            // 最初に設定されている敵の情報を格納する
            for (int i = 0; i < enemys.transform.childCount; i++)
            {
                // オブジェクトデータ取得
                EnemyData enemyData = enemys.transform.GetChild(i).gameObject.GetComponent<EnemyData>();
                // リストに格納
                enemyList.Add(enemyData);
            }
        }
    }

    // 敵生成
    public void SpawnEnemy(Vector3 pos)
    {
        Quaternion quaternion = new Quaternion();
        Instantiate(enemyObject, pos, quaternion, enemys.transform);
        Debug.Log("敵を生成(" + enemyObject.name + ") 座標 " + pos);
    }

    // 敵生成 (オーバーロード)
    public void SpawnEnemy(Vector2 pos)
    {
        Vector3 v3pos = new Vector3(pos.x, pos.y, 0.0f);
        SpawnEnemy(v3pos);
    }

    // 重複なしの乱数を生成
    public int[] uniqRandAry(int min, int max, int length)
    {
        return Enumerable.Range(min, max).OrderBy(n => Guid.NewGuid()).Take(length).ToArray();
    }
}
