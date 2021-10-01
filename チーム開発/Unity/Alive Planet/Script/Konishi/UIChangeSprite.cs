using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeSprite : MonoBehaviour
{
    // SpriteRenderer �R���|�[�l���g���i�[����ϐ�
    private SpriteRenderer m_spriteRenderer;
    // Image �R���|�[�l���g���i�[����ϐ�
    private Image m_image;
    // �X�v���C�g�I�u�W�F�N�g���i�[����z��
    public Sprite[] m_Sprite;
    // �X�v���C�g�I�u�W�F�N�g��ύX���邽�߂̃t���O
    bool Change;
    // ���Ԍv���p�ϐ�
    float time;
    // �I�����ԕϐ�
    public float endTime;
    // UI��
    public bool isUI = false;

    // �Q�[���J�n���Ɏ��s���鏈��
    void Start()
    {
        // �X�v���C�g�I�u�W�F�N�g��ύX���邽�߂̃t���O�� false �ɐݒ�
        Change = false;

        // UI�ɂ�SpriteRenderer�������̂ŁAUI���ǂ����𔻒�
        if(isUI)
        {
            // Image �R���|�[�l���g���擾���ĕϐ� m_image �Ɋi�[
            m_image = GetComponent<Image>();
        }
        else
        {
            // SpriteRenderer �R���|�[�l���g���擾���ĕϐ� m_spriteRenderer �Ɋi�[
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // �Q�[�����s���ɖ��t���[�����s���鏈��
    void Update()
    {
        // ���Ԍo��
        time += Time.deltaTime;

        // �I�����Ԃ܂Ōo������
        if (time > endTime)
        {
            // ���ԃ��Z�b�g
            time = 0;

            // �X�v���C�g�I�u�W�F�N�g�̕ύX�t���O�� true �̏ꍇ
            if (Change)
            {
                // �X�v���C�g�I�u�W�F�N�g�̕ύX
                //�i�z�� m_Sprite[0] �Ɋi�[�����X�v���C�g�I�u�W�F�N�g��ϐ� m_spriteRenderer �Ɋi�[����SpriteRenderer �R���|�[�l���g�Ɋ��蓖�āj
                if (isUI) { m_image.sprite = m_Sprite[0]; }
                else { m_spriteRenderer.sprite = m_Sprite[0]; }

                Change = false;
            }
            // �X�v���C�g�I�u�W�F�N�g�̕ύX�t���O�� false �̏ꍇ
            else
            {
                // �X�v���C�g�I�u�W�F�N�g�̕ύX
                //�i�z�� m_Sprite[1] �Ɋi�[�����X�v���C�g�I�u�W�F�N�g��ϐ� m_spriteRenderer �Ɋi�[����SpriteRenderer �R���|�[�l���g�Ɋ��蓖�āj
                if (isUI) { m_image.sprite = m_Sprite[1]; }
                else { m_spriteRenderer.sprite = m_Sprite[1]; }
                Change = true;
            }
        }
    }
}
