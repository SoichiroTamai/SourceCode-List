using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{
    private enum StatusNum
    {
        Lack,//���Ȃ�
        middle,//����
        Excess,//����
    }

    //�F���D�̍ŏ��l�ƍő�l
    public int minShip = 2;
    public int maxShip = 5;

    //�z�u����ő啝�����ƍŏ�������
    public float minStageX = -25;
    public float maxStageX = 25;
    public float minStageY = 0;
    public float maxStageY = 25;
    public int HierarchyNum = 5;

    //Y�̒l�̍ő�l
    private float MaxPointY = 0;

    //�f���ƉF���D�̃v���n�u
    public ShipStatus MinShipObject;
    public ShipStatus MedShipObject;
    public ShipStatus ManyShipObject;
    public PlanetStatus PlanetObject;

    //�F���D�̎��ނ̍Œ�l�ƍő�l
    public int minStatus = 1;
    public int medStatus = 5;
    public int maxStatus = 10;

    //�F���D��Ŏ��ނ����������Ȃ�����̐�
    public int minOneStatus = 5;
    public int manyOneStatus = 7;

    //�~�ς��g�����ǂ���
    public int ReliefMinNum = 30;//���Ȃ���
    public int ReliefManyNum = 60;//������
    private int AddStatus = 0;
    private bool ReliefFlg = false;

    private StatusNum RelieStatus = StatusNum.middle;

    //�v���n�u���q�I�u�W�F�N�g�ɂ��邽�߂̃I�u�W�F�N�g
    private ShipStatus Obj;
    public GameObject Parent;

    //�������邩�ǂ����̕ϐ����Ǘ����Ă���Ă���Q�[���}�X�^�[�N
    public GameMasterScript masterScript;

    // Start is called before the first frame update
    void Start()
    {
         for (int i = 0; i < HierarchyNum; i++)
         {
            CreateShip(i);
         }

         CreatePlanet();

         masterScript.CreateShip = false;
    }

    //�F���D�̐���
    void CreateShip(int Hierarchy)
    {
        //�F���D�̐��������肷��
        int ShipCount = Random.Range(minShip, maxShip);

        //���肵���F���D�̐����F���D�𐶐�
        for (int i = 0; i < ShipCount; i++)
        {
            float PointX = Random.Range(minStageX, maxStageX);
            float PointY = Random.Range(minStageY, maxStageY) + (maxStageY * Hierarchy);

            //�ړI�n�ݒ�̂���Y���̍ő�l������Ă���
            if(PointY>MaxPointY)
            {
                MaxPointY = PointY;
            }

            int Resource = 0;
            int Fuel = 0;

            //���\�[�X�̗ʂ��쐬�����̐������v���Ă���
            if (RelieStatus==StatusNum.Lack)//�O�̗񂪏��Ȃ����
            {
                Resource = Random.Range(medStatus, maxStatus);
                Fuel = Random.Range(medStatus, maxStatus);
            }
            else if(RelieStatus==StatusNum.Excess)//�O�̗񂪑����ꍇ
            {
                Resource = Random.Range(minStatus, medStatus);
                Fuel = Random.Range(minStatus, medStatus);
            }
            else//����ȊO�̏ꍇ�͍ő�l����ŏ��l�܂łŉ�
            {
                Resource = Random.Range(minStatus, maxStatus);
                Fuel = Random.Range(minStatus, maxStatus);
            }

            AddStatus += Resource + Fuel;

            //���\�[�X�̗ʂƋ��ɃI�u�W�F�N�g���쐬
            if ((Resource + Fuel) < minOneStatus)
            {
                MinShipObject.CreateStatus(Resource, Fuel);
                Obj=Instantiate(MinShipObject, new Vector3(PointX, PointY, 0), Quaternion.identity);
                Obj.transform.parent = Parent.transform;
            }
            else if ((Resource + Fuel) > manyOneStatus)
            {
                ManyShipObject.CreateStatus(Resource, Fuel);
                Obj = Instantiate(ManyShipObject, new Vector3(PointX, PointY, 0), Quaternion.identity);
                Obj.transform.parent = Parent.transform;
            }
            else
            {
                MedShipObject.CreateStatus(Resource, Fuel);
                Obj = Instantiate(MedShipObject, new Vector3(PointX, PointY, 0), Quaternion.identity);
                Obj.transform.parent = Parent.transform;
            }
        }

        //�~�ς��g��������Ⴉ������~�ϑ[�u�����s
        if(ReliefMinNum>AddStatus)
        {
            RelieStatus = StatusNum.Lack;
        }
        else if(ReliefManyNum<AddStatus)//������Ώ��Ȃ��Ȃ�悤�ɐݒ�
        {
            RelieStatus = StatusNum.Excess;
        }
        else//����ȊO�͕��ʂ�
        {
            RelieStatus = StatusNum.middle;
        }

        Debug.Log(RelieStatus.ToString());
        Debug.Log(AddStatus);

        //���v�n�̃��Z�b�g
        AddStatus = 0;
    }

    void CreatePlanet()
    {
        //�ړI�n�̘f���̍��W����萶������
        float PointX = Random.Range(minStageX, maxStageX);
        float PointY = Random.Range(minStageY, maxStageY);

        Instantiate(PlanetObject, new Vector3(PointX, PointY+MaxPointY, 0), Quaternion.identity);
    }
}
