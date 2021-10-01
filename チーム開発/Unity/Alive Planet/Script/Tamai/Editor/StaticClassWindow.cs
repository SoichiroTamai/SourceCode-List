/* 指定したstaticObjectをリストにて閲覧可能する */

using UnityEngine;
using UnityEditor;

public class StaticClassWindow : EditorWindow
{
	private Vector2 _scroll;
	private bool foldout = false;

	[MenuItem("Custom/Static Class Window")]
	static void ShowWindow()
	{
		EditorWindow.GetWindow<StaticClassWindow>();
	}

	private void Update()
	{
		// 変数が更新された時に即座に表示をアップデート
		Repaint();
	}

	private void OnGUI()
    {
        // GameMasterのドローン情報
        if (GameMasterScript.i_DroneDataList != null)
        {
            // 項目数が多い時に自動的にスクロールバーを出す
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            foldout = EditorGUILayout.Foldout(foldout, "i_DroneDataList");

            if (foldout)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < GameMasterScript.i_DroneDataList.Count; i++)
                {
                    // 任意のオブジェクトの Type を表示するフィールドを作成
                    EditorGUILayout.ObjectField("i_DroneDataList " + i, GameMasterScript.i_DroneDataList[i], typeof(GameMasterScript), true);
                }
                EditorGUI.indentLevel--;
            }


            // 項目数が多い時に自動的にスクロールバーを出す
            EditorGUILayout.EndScrollView();
        }
    }
}