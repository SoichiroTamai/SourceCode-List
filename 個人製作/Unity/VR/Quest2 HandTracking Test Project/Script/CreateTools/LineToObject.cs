using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToObject : MonoBehaviour
{
    struct lineCorrection
    {
        public Vector3 pos;
        public bool isCorrection;   // �␳�̗L�� (true = �␳����) �� �ǂݔ�΂�
        public bool isCorner;       // �p���ǂ���
    }

    private lineCorrection[] lineCorrections;
    private DrawLine drawLine;
    private float nearPointDetectValue;

    // �m�F�p
    List<GameObject> _spheres = new List<GameObject>();

    private void Awake()
    {
        drawLine = GetComponent<DrawLine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nearPointDetectValue = 0.01f * 0.01f; //1cm�ȉ��͎�̐k���Ƃ݂Ȃ�

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���C���␳
    public void LineCorrection()
    {
        if (drawLine.GetRenderer().positionCount <= 0) { return; }

        lineCorrections = new lineCorrection[drawLine.GetRenderer().positionCount];

        lineCorrections[0].pos = drawLine.GetRenderer().GetPosition(0);
        lineCorrections[0].isCorrection = false;

        for (int i = 1; i < lineCorrections.Length; i++)
        {
            lineCorrections[i].pos = drawLine.GetRenderer().GetPosition(i);

            var currentVec = (lineCorrections[i].pos - lineCorrections[i-1].pos);

            // �ړ��ʂ����Ȃ��ꍇ�̓��C���̒��_�Ƃ݂Ȃ��Ȃ�
            if(currentVec.sqrMagnitude <= nearPointDetectValue)
            {
                lineCorrections[i].isCorrection = true;
            }
            else
            {
                // �m�F�p
                //CorrecPosCreateObj(i);

                // �p����

            }
        }

        Debug.Log("LineCorrection");
    }

    // ���_�m�F�p �I�u�W�F�N�g����
    private void CorrecPosCreateObj(int num)
    {
        //  Sphere�𐶐����{�[���Ɋ��蓖�Ă�
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = lineCorrections[num].pos;
        sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        Color color = new Color(0.0f, 0.0f, 1.0f, 1.0f);

        sphere.GetComponent<Renderer>().material.color = color;

        _spheres.Add(sphere);
    }

    //�����Ȃ�
    //private void CorrecLineDraw()
    //{
    //    LineRenderer render = drawLine.GetRenderer();

    //    for (int i = 0; i < lineCorrections.Length-1; i++)
    //    {
    //        int NextPositionIndex = render.positionCount;
    //        render.positionCount = NextPositionIndex + 1;
    //        render.SetPosition(i, lineCorrections[i].pos);
    //    }
    //}
}