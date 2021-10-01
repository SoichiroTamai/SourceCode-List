using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnMouse : MonoBehaviour
{
    // ���蓖�Ă�}�e���A��
    public Material[] materialList;
    // �X�v���C�g�I�u�W�F�N�g��ύX���邽�߂̃t���O
    bool Change;
    
    // �}�E�X�J�[�\�����I�u�W�F�N�g�ɏd�Ȃ�����
    private void OnMouseEnter()
    {
        Debug.Log("�^�b�`");
        changeMaterial();
    }

    // �}�E�X�J�[�\�����I�u�W�F�N�g���痣�ꂽ�Ƃ�
    private void OnMouseExit()
    {
        changeMaterial();
    }

    // �}�e���A���ύX
    public void changeMaterial()
    {
        // �X�v���C�g�I�u�W�F�N�g�̕ύX�t���O�� true �̏ꍇ
        if (Change)
        {
            // �X�v���C�g�I�u�W�F�N�g�̕ύX
            //�i�z�� m_Sprite[0] �Ɋi�[�����X�v���C�g�I�u�W�F�N�g��ϐ� m_spriteRenderer �Ɋi�[����SpriteRenderer �R���|�[�l���g�Ɋ��蓖�āj
            GetComponent<Renderer>().material = materialList[0];
            Change = false;
        }
        // �X�v���C�g�I�u�W�F�N�g�̕ύX�t���O�� false �̏ꍇ
        else
        {
            // �X�v���C�g�I�u�W�F�N�g�̕ύX
            //�i�z�� m_Sprite[1] �Ɋi�[�����X�v���C�g�I�u�W�F�N�g��ϐ� m_spriteRenderer �Ɋi�[����SpriteRenderer �R���|�[�l���g�Ɋ��蓖�āj
            GetComponent<Renderer>().material = materialList[1];
            Change = true;
        }
    }

    void Start()
    {
        // �X�v���C�g�I�u�W�F�N�g��ύX���邽�߂̃t���O�� false �ɐݒ�
        Change = false;
    }
}
