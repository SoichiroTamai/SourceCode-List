using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    // �\��������Z�[�u�t�@�C��
    public List<Button> SaveFileList;

    public Button gameStart;

    public void ButtonClick()
    {
        // ��\����
        gameStart.gameObject.SetActive(false);

        // ���[�h��ʂ�\��
        for (int i = 0; i < SaveFileList.Capacity; i++)
        {
            SaveFileList[i].gameObject.SetActive(true);
        }
    }
}
