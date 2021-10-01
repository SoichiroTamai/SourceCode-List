package com.example.myapplication;

import java.util.ArrayList;

public class MapJsonData {

    public ArrayList<MapInfo> MapData;  // Mapデータ

    class MapInfo {
        public String   Map_No;         // マップ番号
        public Vector2  StartPosition;  // 車の初期座標 （マップ上での座標）
        public int      TargetTime;     // 目標タイム （秒）
    }
}
