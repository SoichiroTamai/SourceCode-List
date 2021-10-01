using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DroneRepair : MonoBehaviour
{
    [SerializeField] Drone.DroneData droneData;
    [SerializeField] int fuelMax;   // �ő�R��
    [SerializeField] int fuel;      // �R��
    [SerializeField] int resMax;    // �ő厑��

    public MotherShip motherShip;

    //���\�[�X�Ƃ����Ǘ�����Q�[���I�u�W�F�N�g
    public GameMasterScript masterScript;

    public Slider resSlider;

    //�C���o���Ȃ����ɕ\������UI
    public Image image;
    private bool imageFlg = false;

    private float FadeSpeed = 0.003f;//�����x���ς��X�s�[�h���Ǘ�
    float alpha = 1;                      //�p�l���̕s�����x���Ǘ�

    public Text ResourceText;


    // �C��
    public void Repair()
    {
        if (fuel < fuelMax || droneData.droneRes < resMax)
        {
            // �����
            int resConsume = (int)resSlider.value;

            // �C���ɕK�v�ȗʂ���������
            if (masterScript.ResouceVault>= resConsume)
            {

                if (resMax - droneData.droneRes < resConsume)
                {
                    resConsume = resMax - droneData.droneRes;
                }

                // ������[
                Debug.Log("�����O"+masterScript.ResouceVault);
                masterScript.ResouceVault = masterScript.ResouceVault - resConsume;
                droneData.droneRes += resConsume;
                Debug.Log("������" + masterScript.ResouceVault);
            }
            else
            {
                Debug.Log("�����o���܂���");
                // �o���s�\
                image.gameObject.SetActive(true);
                imageFlg = true;
                alpha = 1;
            }
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

    //�Z�b�^�[�ƃQ�b�^�[
    public int GetFuel() { return fuel; }

    public int GetRes() { return droneData.droneRes; }

    public void SetFuel(int Num) { fuel = Num; }

    public void SetRes(int Num) { droneData.droneRes = Num; }

    // Start is called before the first frame update
    void Start()
    {
        fuelMax = 10;
        resMax = 5;
        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            fuel = 0;
            droneData.droneRes = 0;
        }

        if (imageFlg)
        {
            FadeOut();
        }

        ResourceText.text = "���ށF" + masterScript.ResouceVault;

        if(masterScript.ResouceVault >= (int)resSlider.value)
        {
            ResourceText.color = Color.white;
        }
        else
        {
            ResourceText.color = Color.red;
        }
    }
}
