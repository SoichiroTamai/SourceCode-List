// 継承したクラスのインスペクターウィンドウにある指定した項目のみ非表示にする

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RecordingCoordinates_CustomUIEditor : MöbiusSystem_RecordingEffect
{
}

#if UNITY_EDITOR
    // 修正したいインスペクターを指定
    [CustomEditor(typeof(MöbiusSystem_RecordingEffect), true)]
    public class CustomTeleportEditor : Editor
    {
        // カスタムインスペクターを上書き作成
        public override void OnInspectorGUI()
        {
            // 最後のUpdateの呼び出しやスクリプトを実行した後、オブジェクトが変更された場合のみ シリアライズされたオブジェクト表示を更新
            this.serializedObject.UpdateIfRequiredOrScript();

            // 1つ目のシリアライズ化されたプロパティーを取得
            var iterator = this.serializedObject.GetIterator();
            for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                // caseで指定した名称のプロパティがあれば、それは表示せず次のプロパティに進む
                switch(iterator.propertyPath)
                {
                    case "key_Record":
                    case "key_Play":
                    case "key_Reset":
                    case "RecordableTime":
                    case "MöbiusSprite":
                    case "CatchObject":
                    case "Recorded_Particle":

                        continue;
                }

                using (new EditorGUI.DisabledScope(iterator.propertyPath == "m_Script"))
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }
            this.serializedObject.ApplyModifiedProperties();
        }
    }
#endif