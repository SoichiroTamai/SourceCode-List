/*
　　インスペクター欄でタグ指定が出来る用にする
 
　　- 使用方法 -
	使いたい変数 (string型) に [Tag] 属性を付与する

	例：[Tag] public string targetTag;

 */

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// 継承元(PropertyAttribute) → カスタムプロパティー属性を派生させるベースクラス
public class TagAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagAttribute))]
// PropertyDrawer → カスタムプロパティードローワーの基底クラス
public class TagDrawer : PropertyDrawer {

	// GUI イベントのハンドリング
	// ※ MonoBehaviourのenabledプロパティがfalseに設定されている場合、OnGUI（）は呼び出されない
	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {

		EditorGUI.BeginProperty (position, label, prop);

		prop.stringValue = EditorGUI.TagField(position, label, prop.stringValue);

		EditorGUI.EndProperty ();
	}
}
#endif