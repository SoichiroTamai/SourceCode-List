//package com.example.myapplication;
//import java.util.Map;
//import java.util.HashMap;
//
//public class LadderDiagramFactory {
//
//   // 既に生成したStampインスタンスを管理
//    Map<Character, Stamp> pool;
//    LadderDiagramFactory(){
//        this.pool = new HashMap<Character, Stamp>();
//    }
//    Stamp get(char type){
//        Stamp GraphicSymbol = this.pool.get(type);
//        if(GraphicSymbol == null) {
//            GraphicSymbol = new Stamp(type);
//            this.pool.put(type, GraphicSymbol);
//        }
//        return GraphicSymbol;
//    }
//}