///*
//   - 使用例 - 特定のタグと接触したらオブジェクトを破棄する
    
//    // ※変数名は固定
//    public string trigger_tag;
 
//    public void OnTriggerEnter(Collider c){
//        if (c.gameObject.tag == trigger_tag) {
//            Destroy(gameObject);
//        }
//    }  
// */

//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using Drone;

//[CustomEditor(typeof(DroneManager))]
//public class TagSelecter : Editor
//{
//    // オブジェクトのプロパティを編集するためのクラス
//    // ※ Unity5.3から SerializedProperty を使用した形式でないと値が保存されない
//    SerializedProperty trigger_tag;

//    // オブジェクトが有効/アクティブになったときに呼び出す
//    void OnEnable()
//    {
//        // 検査されるオブジェクトの SerializedObject を作成
//        trigger_tag = serializedObject.FindProperty("trigger_tag");
//    }

//    // カスタムインスペクターを作成するため関数
//    public override void OnInspectorGUI()
//    {
//        // 元の処理を呼び出す
//        base.OnInspectorGUI();

//        serializedObject.Update();

//        // タグを選択するフィールドを作成
//        trigger_tag.stringValue = EditorGUILayout.TagField("Trigger Tag", trigger_tag.stringValue);

//        // プロパティーの変更を適用
//        serializedObject.ApplyModifiedProperties();
//    }
//}