using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinder
{
    // マスの状態
    public enum statusData
    {
        none,
        open,
        closed
    }

    // パス(マップのマス)情報
    public class PathNode : MonoBehaviour
    {
        public GameObject tileObj;// マス目の状態
        public statusData status; // マス目の状態

        public Vector2 pos; // 座標

        public float gCost;      // 実コスト   ( 開始位置からどれだけ離れているか ※開始位置から何タイル離れているか )
        public float hCost;      // 推定コスト ( 終了位置からどれだけ離れているか ※マンハッタン距離 )
        public float fCost; // スコア     ( gCost + hCost ) 

        public PathNode parentNode; // 親ノード

        private void Reset()
        {
            fCost = -1;
        }

        private void Start()
        {
            Reset();
        }

        // 実コストを加算
        void Add_gCost() { gCost++; }

        // 推定コストを計算 (基準ノード,ゴール地点)
        public void Calculate_hCost(Vector2 referenceNodePos, Vector2 goalPos)
        {
            // 斜め移動ありでコストを求める
            float x = Mathf.Abs(goalPos.x - referenceNodePos.x);
            float y = Mathf.Abs(goalPos.y - referenceNodePos.y);

            //// 高い方を推定コストにする
            //if (x > y) { hCost = x; }
            //else { hCost = y; }

            // 平均値を推定コストに
            hCost = (x + y)/2.0f;
        }

        // スコアを計算
        public void Calculate_fCost()
        {
            fCost = gCost + hCost;
        }
    }
}