using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class ChangeSceneButton : MonoBehaviour
{
    public GameObject ClickObject;

    public GameObject ShipAnimObj;

    public GameObject BlackMonitor;

    private SceneShipAnimScript SSAP;

    private BlackFrontScript BFS;

    private GetClickObject clickObject;

    //�v���C���[�̃I�u�W�F�N�g
    public SelectRootPlayer player;
    //�����_�[�p�C�v���C���̃A�Z�b�g
    public RenderPipelineAsset pipelineAsset;
    //�v���C���[�͈ړ����J�n���邩�ǂ���
    private bool MoveFlg = false;

    public Image image;
    private bool imageFlg = false;

    public GameMasterScript masterScript;

    private float FadeSpeed = 0.003f;//�����x���ς��X�s�[�h���Ǘ�
    float alpha = 1;                      //�p�l���̕s�����x���Ǘ�

    private void Start()
    {
        if (!ShipAnimObj) { return; }
        if (!BlackMonitor) { return; }
        if (!ClickObject) { return; }

        SSAP = ShipAnimObj.GetComponent<SceneShipAnimScript>();
        BFS = BlackMonitor.GetComponent<BlackFrontScript>();
        clickObject = ClickObject.GetComponent<GetClickObject>();

        //alpha = image.color.a;
        image.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (BFS.GetCompFlg())
        //{
        //    SceneManager.LoadScene("MapCreateTest");
        //}

        if(MoveFlg)
        {
            //�v���C���[���ړ�������
            if(player.MovePlayer())
            {
                //�S�[���n�_�ɓ��B���Ă���΃����_�[�p�C�v���C����ݒ肵���̃V�[���Ɉڂ�
                MoveFlg = false;
                SceneManager.LoadScene("MapCreateTest");
            }
        }

        if(imageFlg)
        {
            FadeOut();
        }
    }

    public void OnClick()
    {
        if (!player.CanMove())
        {
            if (clickObject.GetClickedObject().tag == "Ship")
            {
                //SSAP.AnimationOn();
                MoveFlg = true;
                masterScript.FuelVault -= player.SelectObjectDistance();

            }
            else if (clickObject.GetClickedObject().tag == "Planet")
            {
                SceneManager.LoadScene("EDScene");
            }
        }
        else
        {
            image.gameObject.SetActive(true);
            imageFlg = true;
            alpha = 1;
        }
    }

    void FadeOut()
    {
        alpha -= FadeSpeed;//�s�����x�����X�ɏグ��
        image.color = new Color(image.color.r, image.color.g,
            image.color.b, alpha);//�ύX�����s�����x���p�l���ɔ��f����

        //0�ȉ��ɂȂ����珈���I��
        if (alpha <= 0)
        {
            imageFlg = false;
            image.gameObject.SetActive(false);
        }
    }
}
