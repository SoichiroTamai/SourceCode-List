using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRootPlayer : MonoBehaviour
{
    public GetClickObject getClickObject;

    //���ނ̗ʂƂ����Ǘ����Ă���X�N���v�g
    public GameMasterScript masterScript;

    //������\������e�N�X�`��
    public Text text;
    //�Q�[���}�l�[�W���[
    public GameMasterScript GameMaster;
    //��s�@�̈ړ�����X�s�[�h
    public float speed = 0.05f;
    //��s�@�̈ړ�����O�̈ʒu
    private Vector3 StartPos;
    //���݂̔R����\������e�L�X�g
    public Text FuelVaultText;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GameMaster.PlayerPos;
        FuelVaultText.text ="�R���F" + masterScript.FuelVault.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(getClickObject.GetClickedObject()==null)
        {
            return;
        }

        text.text=string.Format("{000}m", SelectObjectDistance()) ;

        if(CanMove())
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }

    //�N���b�N���Ă���I�u�W�F�N�g�ƃv���C���[�̋�����Ԃ��֐�
    public int SelectObjectDistance()
    {
        Vector3 PlayerPos = this.transform.position;
        GameObject ClickObject = getClickObject.GetClickedObject();
        Vector3 ClickObjectPos = ClickObject.transform.position;
        float distance = Vector3.Distance(PlayerPos, ClickObjectPos);

        return (int)distance;
    }

    public void SetStartPos()
    {
        StartPos = this.transform.position;
    }

    public bool CanMove()
    {
        return SelectObjectDistance() > masterScript.FuelVault;
    }
    
    //���̊֐���Update�ŌĂׂΓ���
    public bool MovePlayer()
    {
        //�v���C���[�̍��W���N���b�N���Ă���I�u�W�F�N�g�ɍ��킹��
        //this.transform.position = getClickObject.GetClickedObject().transform.position;
        //���킹�����W���Q�[���}�l�[�W���[�ɓn��
        //GameMaster.PlayerPos = this.transform.position;

        //���݂̈ʒu
        float present_Location = (Time.time * speed) / SelectObjectDistance();
        //�I�u�W�F�N�g�̈ړ�
        this.transform.position = Vector3.Lerp(StartPos, getClickObject.GetClickedObject().transform.position, present_Location);
        
        //�I�u�W�F�N�g���S�[���܂Œ�������
        if(present_Location>=1)
        {
            //���̏ꏊ���i�[���������I��
            GameMaster.PlayerPos = this.transform.position;
            return true;
        }

        return false;
    }
}
