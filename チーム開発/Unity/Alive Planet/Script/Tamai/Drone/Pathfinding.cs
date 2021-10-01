/*
  参照サイト
  https://qiita.com/2dgames_jp/items/f29e915357c1decbc4b7#%E3%82%B5%E3%83%B3%E3%83%97%E3%83%AB%E3%82%B3%E3%83%BC%E3%83%89%E3%81%AE%E7%B0%A1%E5%8D%98%E3%81%AA%E8%AA%AC%E6%98%8E

    【 経路探索 】
     Get_Pathfinding()
    
    ・Pathfinding_MapDataCopy() // 経路探索に必要なマップ情報を取得　※Map生成後にする
    ・Pathfinding_Map()         // 経路探索を実行
    ・path_Goal                 // ゴールまでの経路が格納されている
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinder
{
    // 指定したタイル間の経路探索
    public class Pathfinding : MonoBehaviour
    {
        private MapCreate mapCreate;
        private List<PathNode> _mapBlockList;
        
        private int searchTile_layerMask;

        // 開示時に基準となるタイル(中心)
        private PathNode parenNode;

        //スタート地点
        private Vector2 startTilePos;

        // ゴール地点
        private int goalTileNum;
        
        // 回収予定のリソース
        private List<GameObject> target_Resources;

        // 経路
        public  List<Vector2>  path_Goal;   // スタートからゴールまでの経路
        private List<PathNode> path_Closed; // 閉鎖したタイル

        public Vector3 test_StartPos;
        public Vector3 test_GoalPos;

        bool test_KeyFlg;

        private void Reset()
        {
            parenNode = null;
            path_Goal.Clear();
            path_Closed.Clear();

            mapCreate = GameObject.Find("MapCreateManager").GetComponent<MapCreate>();
            mapCreate.MapBlockList.AddRange(mapCreate.MapBlockList_route);
        }

        private void Start()
        {
            // タイル検索用レイマスク
            searchTile_layerMask = 1 << LayerMask.NameToLayer("MapTile");

            path_Goal = new List<Vector2>();
            path_Closed = new List<PathNode>();
            startTilePos = new Vector2();

            Reset();
        }

        private void Update()
        {
            // デバック用 <-----------------------------------
            if (Input.GetKey(KeyCode.Space))
            {
                if (test_KeyFlg) { return; }

                test_KeyFlg = true;
                Get_Pathfinding(test_StartPos, test_GoalPos);
            }
            else
            {
                test_KeyFlg = false;
            }
            // --------------------------------------------->
        }

        public List<Vector2> Get_Pathfinding(int starTileNum, int goalTileNum)
        {
            Reset();

            Debug.Log("経路探索開始");

            Pathfinding_MapDataCopy();
            Pathfinding_Map(starTileNum, goalTileNum);

            return path_Goal;
        }

        // オーバーロード (座標指定)
        public List<Vector2> Get_Pathfinding(Vector3 startPos, Vector3 goalPos)
        {
            Reset();

            Debug.Log("経路探索開始");

            Vector2 start = new Vector2(startPos.x, startPos.y);
            Vector2 goal = new Vector2(goalPos.x, goalPos.y);

            int starTileNum = getTileNum(start);
            if(starTileNum < 0) { Debug.Log("経路探索：スタート地点にマップタイルが見つかりませんでした。"); }
            
            int goalTileNum = getTileNum(goal);
            if (goalTileNum < 0) { Debug.Log("経路探索：ゴール地点にマップタイルが見つかりませんでした。"); }

            Pathfinding_MapDataCopy();
            Pathfinding_Map(starTileNum, goalTileNum);

            return path_Goal;
        }

        // リソースの場所までの経路を作成
        public List<Vector2> Get_Pathfinding(Vector3 startPos, GameObject resourceObj)
        {
            if(resourceObj == null) 
            {
                Debug.LogWarning("オブジェクトが設定されていない状態では、この関数を使用しての経路探索は出来ません");
                return null;
            }

            Get_Pathfinding(startPos, resourceObj.transform.position);

            return path_Goal;
        }

        // 必要なマップ情報を取得
        void Pathfinding_MapDataCopy()
        {
            _mapBlockList = new List<PathNode>();

            foreach (var mapData in mapCreate.MapBlockList)
            {
                PathNode node = new PathNode();
                //PathNode node = gameObject.AddComponent<PathNode>();

                // マップデータ初期化
                node.tileObj = mapData; // タイル情報を格納
                node.status = statusData.none; // statusData.none
                node.pos = new Vector2(mapData.transform.position.x, mapData.transform.position.y);
                node.parentNode = null;

                // 検索用マップに追加
                _mapBlockList.Add(node);

                node = null;
                //Destroy(node);
                //Destroy(GetComponent<PathNode>());
            }

            path_Goal = new List<Vector2>();
            path_Closed = new List<PathNode>();
        }

        // 経路探索 (A*)   (開始地点の要素番号, ゴール地点の番号)
        void Pathfinding_Map(int starTileNum, int _goalTileNum)
        {
            // 目標地点を設定
            goalTileNum = _goalTileNum;

            // 開始地点を記憶
            startTilePos = _mapBlockList[starTileNum].pos;

            // 基準タイルを開始地点に設定
            parenNode = _mapBlockList[starTileNum];

            // 開始地点の実コストを0に
            _mapBlockList[starTileNum].gCost = 0;

            // 開始地点のタイル情報を開示
            Open(starTileNum, _mapBlockList[starTileNum]);

            float _gCost = 0;

            // 基準ノードがゴール地点に着くまで
            while (parenNode.pos != _mapBlockList[_goalTileNum].pos)
            {
                // スコアが一番小さいタイル
                PathNode minNodeData = new PathNode();

                float min_fCost = 9999;
                float min_gCost = 9999;

                // 基準ノードの判定
                foreach (var mapData in _mapBlockList)
                {
                    // open状態でなけれは早期リターン
                    if(mapData.status != statusData.open) { continue; }

                    // スコアの最小値を基に基準ノードを指定
                    if (mapData.fCost > min_fCost)
                    {
                        continue;
                    }

                    // スコアが同じ場合は実コストが小さい方を優先
                    if (mapData.fCost == min_fCost && mapData.gCost > min_gCost)
                    {
                        continue;
                    }

                    // 最小値更新
                    min_fCost = mapData.fCost;
                    min_gCost = mapData.gCost;
                    minNodeData = mapData;

                    //if (parenNode.fCost > mapData.fCost || parenNode.fCost < 0)
                    //{
                    //    // スコアが同じ場合、実コストが小さい方を優先して基準ノードに
                    //    if (parenNode.fCost == mapData.fCost && parenNode.gCost >= mapData.gCost)
                    //    {
                    //        continue;
                    //    }

                    //    minNodeData = mapData;

                    //    //// 次の基準ノードまで戻る
                    //    //path_Goal.RemoveRange(minNodeData.gCost, path_Goal.Count - minNodeData.gCost);
                    //}
                }

                // 更新なし又は close状態なら終了
                if(minNodeData == new PathNode() && minNodeData.status == statusData.closed)
                {
                    return;
                }

                parenNode = minNodeData;

                // 実コスト加算
                _gCost++;

                // 周囲を開示
                urroundingsOpen(_gCost);

                // 基準ノードを閉鎖済み(検索済み)のパス一覧に追加
                if (parenNode.status == statusData.closed)
                {
                    path_Closed.Add(parenNode);
                }

                // 無限ループ対策 ※保険
                if (_gCost>10000) {
                    return;
                }
            }

            // ゴールまでの経路を取得
            GetGoalPath();

            Debug.Log("経路探索終了");
            if (path_Closed.Count > 0) { Debug.Log("スタート地点：" + path_Closed[0].pos); }
            if (path_Goal.Count > 0) { Debug.Log("ゴール地点：" + path_Goal[path_Goal.Count - 1]); }
        }

        // 周囲を開示 (基準のノード) ※中心タイル
        public void urroundingsOpen(float _gCost)
        {
            Vector2 parentPos = parenNode.pos;

            // 周囲のタイル番号を検索

            /*
              searchNumの開示場所

                012
                3 4
                567
            */

            int[] searchNum = new int[8];

            searchNum[0] = tileSearch(parentPos.x - 1, parentPos.y + 1);
            searchNum[1] = tileSearch(parentPos.x    , parentPos.y + 1);
            searchNum[2] = tileSearch(parentPos.x + 1, parentPos.y + 1);
            searchNum[3] = tileSearch(parentPos.x - 1, parentPos.y);
            searchNum[4] = tileSearch(parentPos.x + 1, parentPos.y);
            searchNum[5] = tileSearch(parentPos.x - 1, parentPos.y - 1);
            searchNum[6] = tileSearch(parentPos.x    , parentPos.y - 1);
            searchNum[7] = tileSearch(parentPos.x + 1, parentPos.y - 1);

            // 開示
            foreach (var tileNum in searchNum)
            {
                // タイルがあれば開示
                if (tileNum > 0)
                {
                    if(_mapBlockList[tileNum].status == statusData.closed){ continue; }

                    _mapBlockList[tileNum].gCost = _gCost;
                    Open(tileNum, parenNode);
                }
            }

            // 中心(基準)のタイルを閉鎖
            parenNode.status = statusData.closed;
        }

        // タイル番号を取得
        public int tileSearch(float x, float y)
        {
            Vector2 pos = new Vector2(x, y);

            return getTileNum(pos);
        }

        // タイル情報を開示
        void Open(int openTileNum, PathNode parentPathNode = null)
        {
            // 開示済みの場合は除く
            if(_mapBlockList[openTileNum].status != statusData.none) { return; }

            // タイル情報を開示状態に変更
            _mapBlockList[openTileNum].status = statusData.open;

            // 推定コストを計算
            _mapBlockList[openTileNum].Calculate_hCost(_mapBlockList[openTileNum].pos, _mapBlockList[goalTileNum].pos);

            // スコア計算
            _mapBlockList[openTileNum].Calculate_fCost();

            // 親ノードを設定
            _mapBlockList[openTileNum].parentNode = parentPathNode;
        }

        /*
             hitTileNum… 指定した座標にあるタイルの添え字を返す ※ -1 (見つからなかった)
        */

        // 指定した座標のタイル番号を取得
        int getTileNum(Vector2 pos)
        {
            //bool otherObject = false;   // タイル以外のオブジェクトがあった

            int hitTileNum = -1;

            // 見つかった座標
            GameObject hitGameObject = null;

            // レイを作成 ※ MapTile のみ 
            Vector2 vector2dir = new Vector2(0, 0);
            //RaycastHit2D hit2d = Physics2D.Raycast(pos, vector2dir, Mathf.Infinity, searchTile_layerMask);
            RaycastHit2D hit2d = Physics2D.Raycast(pos, vector2dir, Mathf.Infinity,
                                                   ~(1 << LayerMask.NameToLayer("Resource") | 1 << LayerMask.NameToLayer("Drone") | 1 << LayerMask.NameToLayer("UI")));
            //RaycastHit2D hit2d = Physics2D.Raycast(pos, vector2dir);

            // 当たったら
            if (hit2d)
            {
                if (hit2d.transform.gameObject.tag == "MapTile")
                {
                    hitGameObject = hit2d.transform.gameObject;
                }
                else
                {
                    Debug.Log("タイル, ドローン, リソース以外のオブジェクトが見つかった：" + hit2d.transform.gameObject.name);

                    return -1;

                    //RaycastHit2D hit2d2 = Physics2D.Raycast(pos, vector2dir, Mathf.Infinity, searchTile_layerMask);
                    //if(hit2d2)
                    //{
                    //    hitGameObject = hit2d2.transform.gameObject;
                    //    otherObject = true;
                    //}
                }
            }
            else
            {
                // タイルが無かった
                return -1;
            }

            // タイル情報から座標一致するタイルを探し添え字を取得
            for(int i=0; i < mapCreate.MapBlockList.Count; i++)
            {
                if (mapCreate.MapBlockList[i].transform.position == hitGameObject.transform.position)
                {
                    hitTileNum = i;
                }

                // 見つかったら検索終了
                if(hitTileNum >= 0) { break; }
            }

            //if (otherObject)
            //{
            //    _mapBlockList[hitTileNum].status = statusData.closed;
            //}

            // 指定した座標にあるタイルの添え字を返す
            return hitTileNum;
        }

        /// パスを取得する
        public void GetGoalPath()
        {
            if(path_Closed.Count <= 0) { return; }

            // 再起用添え字の変数
            PathNode pathNode = path_Closed[path_Closed.Count-1];

            // 開始地点までの経路を再帰的に座標情報を追加 (親ノードがなくなるまで)
            //while (pathNode.parentNode != null)
            while (pathNode.pos != startTilePos)
            {
                // 座標を格納
                path_Goal.Add(pathNode.pos);

                // 判定ノードを親ノードに変更
                pathNode = pathNode.parentNode;
            }

            // スタート地点からゴールまでの経路にする為反転する
            path_Goal.Reverse();
        }
    }
}