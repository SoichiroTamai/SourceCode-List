using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStatus : MonoBehaviour
{
    //��������m��
    public int Probability;
    public int probability;

    //�����̍ő�l
    public int MaxClearNum = 100;
    //�����̍ŏ��l
    public int MinClearNum = 0;

    //�����������ǂ���
    private bool IsClear;

    // Start is called before the first frame update
    void Start()
    {
        //���p�[�Z���g�Ő������邩������o��
        Probability = Random.Range(MinClearNum, MaxClearNum);

        //���̃p�[�Z���g�Ő������邩�ǂ������o��
        probability = Random.Range(0, 100);

        //�������o���������O�ɏo�������̕����傫����ΐ���
        if(probability<Probability)
        {
            IsClear = true;
        }
        else
        {
            IsClear = false;
        }
    }

    public bool GetIsClear(){ return IsClear; }

    // Update is called once per frame
    void Update()
    {
        
    }
}
