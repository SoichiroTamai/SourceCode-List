/* �w�肵��staticObject�����X�g�ɂĉ{���\���� */

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
		// �ϐ����X�V���ꂽ���ɑ����ɕ\�����A�b�v�f�[�g
		Repaint();
	}

	private void OnGUI()
    {
        // GameMaster�̃h���[�����
        if (GameMasterScript.i_DroneDataList != null)
        {
            // ���ڐ����������Ɏ����I�ɃX�N���[���o�[���o��
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            foldout = EditorGUILayout.Foldout(foldout, "i_DroneDataList");

            if (foldout)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < GameMasterScript.i_DroneDataList.Count; i++)
                {
                    // �C�ӂ̃I�u�W�F�N�g�� Type ��\������t�B�[���h���쐬
                    EditorGUILayout.ObjectField("i_DroneDataList " + i, GameMasterScript.i_DroneDataList[i], typeof(GameMasterScript), true);
                }
                EditorGUI.indentLevel--;
            }


            // ���ڐ����������Ɏ����I�ɃX�N���[���o�[���o��
            EditorGUILayout.EndScrollView();
        }
    }
}