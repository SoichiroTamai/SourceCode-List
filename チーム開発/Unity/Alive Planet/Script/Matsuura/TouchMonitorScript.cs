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

    private GameObject clicked_TargetObject; // クリック選択したオブジェクト

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

        // クリック処理
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clicked_TargetObject = hit2d.transform.gameObject;

                // クリック対象がノイズ状態の時
                if (clicked_TargetObject.tag == "Jammer")
                {
                    return;
                }
                // クリック対象がノイズ状態じゃないとき
                if (clicked_TargetObject.tag == "MapTile")
                {
                    // クリックしたマップチップの座標を取ってきている
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

    // クリックしたマップチップの座標を取得したいとき
    public Vector3 GetClicedMapChipPotision()
    {
        return v_SquareVec;
    }
}
