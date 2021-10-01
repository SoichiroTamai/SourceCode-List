using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Z�[�u�{�^�����������Ƃ��ɃZ�[�u�t�@�C����UI��3�\������X�N���v�g
public class UISaveActive : MonoBehaviour
{
    // ��\���ɂ���I�u�W�F�N�g�̃��X�g
    public List<GameObject> UIActiveFalseList;

    // �\������Z�[�u�t�@�C���̃��X�g
    public List<GameObject> UIActiveTrueList;
    
    public void ButtonClick()
    {
        // �\�����X�g��\��
        for (int i = 0; i < UIActiveTrueList.Count; i++)
        {
            UIActiveTrueList[i].gameObject.SetActive(true);
        }

        // ��\�����X�g���\����
        for(int i = 0; i < UIActiveFalseList.Count; i++)
        {
            UIActiveFalseList[i].gameObject.SetActive(false);
        }
    }
}
