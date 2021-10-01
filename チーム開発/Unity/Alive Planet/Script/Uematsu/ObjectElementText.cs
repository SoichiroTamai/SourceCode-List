using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectElementText : MonoBehaviour
{
    //�N���b�N�����I�u�W�F�N�g
    public GetClickObject clickObject;

    //�D�̏��̃X�N���v�g
    private ShipStatus shipScript;

    //�D�̏���Ԃ��ϐ�
    private ShipStatus hideShipStatus;

    //�f���̏��̃X�N���v�g
    private PlanetStatus planetScript;

    //�e�L�X�g���o������������肷�邽�߂̃I�u�W�F�N�g
    public GameObject ShipTextObject;
    public GameObject PlanetTextObject;

    //�������o���e�L�X�g
    public Text ResourceText;
    public Text FuelText;
    public Text ClearText;

    private void Update()
    {
        //�I�u�W�F�N�g���I������ĂȂ���ΕԂ�
        if(clickObject.GetClickedObject()==null)
        {
            return;
        }

        //�I�����ꂽ�I�u�W�F�N�g�ɑΉ�����X�e�[�^�X��\������
        if(clickObject.GetClickedObject().tag=="Ship")
        {
            TextShip(clickObject.GetClickedObject());
        }
        else if (clickObject.GetClickedObject().tag == "Planet")
        {
            TextPlanet(clickObject.GetClickedObject());
        }
    }

    public void TextShip(GameObject shipObject)
    {
        //�D�̏����o���Ęf���̏�������
        ShipTextObject.SetActive(true);
        PlanetTextObject.SetActive(false);

        //�N���b�N�����I�u�W�F�N�g���Q�Ƃ��ϐ��������o��
        shipScript = shipObject.GetComponent<ShipStatus>();

        hideShipStatus = shipScript;

        //�����o�����������e�L�X�g�ŕ\������
        ResourceText.text = string.Format("Resource:{000}", shipScript.ResourceNum);
        FuelText.text = string.Format("Fuel:{000}", shipScript.FuelNum);
    }

    public void TextPlanet(GameObject planetObject)
    {
        //�f���̏����o���đD�̏�������
        ShipTextObject.SetActive(false);
        PlanetTextObject.SetActive(true);

        //��Ɠ���
        planetScript = planetObject.GetComponent<PlanetStatus>();
        ClearText.text = string.Format("Safe:{000}%", planetScript.Probability);
    }

    public bool GetClear() 
    {
        GameObject planetObject = clickObject.GetClickedObject();
        planetScript = planetObject.GetComponent<PlanetStatus>();
        if (clickObject.GetClickedObject().tag == "Planet")
        {
            TextPlanet(clickObject.GetClickedObject());
            return planetScript.GetIsClear();
        }
        return false;
    }

    public ShipStatus GetShipParam()
    {
        if (!hideShipStatus) { return null; }
        return hideShipStatus;
    }
}
