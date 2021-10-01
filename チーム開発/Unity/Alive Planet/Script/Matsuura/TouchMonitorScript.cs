using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMonitorScript : MonoBehaviour
{
    public GameObject CanvasObject;

    private Vector3 v_SquareVec;

    public bool SquareFlg = false;

    private bool b_flg;

    private int i_Time;

    private GameObject clicked_TargetObject; // �N���b�N�I�������I�u�W�F�N�g

    public GameObject clickObject
    {
        get { return clicked_TargetObject; }
        set { clicked_TargetObject = value; }
    }

    void Start()
    {
        b_flg = false;
        i_Time = 0;
    }

  
    void Update()
    {

        // �N���b�N����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clicked_TargetObject = hit2d.transform.gameObject;

                // �N���b�N�Ώۂ��m�C�Y��Ԃ̎�
                if (clicked_TargetObject.tag == "Jammer")
                {
                    return;
                }
                // �N���b�N�Ώۂ��m�C�Y��Ԃ���Ȃ��Ƃ�
                if (clicked_TargetObject.tag == "MapTile")
                {
                    // �N���b�N�����}�b�v�`�b�v�̍��W������Ă��Ă���
                    v_SquareVec = clicked_TargetObject.transform.position;
                    //Debug.Log(clicked_TargetObject.transform.position);

                }
            }
        }
        else
        {
            clicked_TargetObject = null;
        }

        if (b_flg)
        {
            if (i_Time > 30)
            {
                return;
            }

            Vector3 vec = CanvasObject.transform.position;

            vec.x -= 0.1f;
            i_Time++;

            CanvasObject.transform.position = vec;

        }
        else
        {
            if (i_Time <= 0)
            {
                return;
            }


            Vector3 vec = CanvasObject.transform.position;

            vec.x += 0.1f;
            i_Time--;

            CanvasObject.transform.position = vec;


        }

        if (SquareFlg)
        {
            //Debug.Log(SquareVec);
        }

    }

    public void Flglation()
    {
        if (b_flg)
        {
            b_flg = false;
        }
        else
        {
            b_flg = true;
        }
    }

    public bool GetFlg()
    {
        return b_flg;
    }

    // �N���b�N�����}�b�v�`�b�v�̍��W���擾�������Ƃ�
    public Vector3 GetClicedMapChipPotision()
    {
        return v_SquareVec;
    }
}
