using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectScript : MonoBehaviour
{
    // プライベート//////////////////////////////////////////////////////////////////////////////////////////////////////////

    private Vector3 v_moveTo;                                     // オブジェクト移動行列

    private Vector3 v_savePos;                                    // 座標一時保存変数

    private float f_RayDir;                                     // レイの距離

    private bool b_IsCatch;                                      // レイが発動しているかどうか

    private bool b_rotflg;                                     // 回転するかどうか

    private Rigidbody R_rigidBody;                                  // 重力操作変数

    private float f_scroll;

    private float f_ObjectDir;
    // パブリック////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Camera RayforCamera;                               // カメラ変数

    public MöbiusSystem MSystem;                                    // メビウスシステム変数

    //  本体　初期化
    void Start()
    {
        f_ObjectDir = 1;
        f_RayDir = 4;
        b_IsCatch = false;
        R_rigidBody = GetComponent<Rigidbody>();
        v_savePos = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // 本体　更新
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CatchObject();
        }

        if (b_IsCatch)
        {
            MovePosition();
            MoveRotation();

            R_rigidBody.useGravity = false;
        }
        else
        {
            R_rigidBody.useGravity = true;
            f_ObjectDir = 1;
        }

        if (Input.GetMouseButtonUp(0))
        {
            b_IsCatch = false;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 関数名 RayCheck                                                                                                       //
    /// 引数:void                                                                                                             //
    /// 返り値:bool                                                                                                           //
    /// 機能：カメラの中心からレイを飛ばし、そのレイの距離内に掴めるオブジェクトがあった場合trueを返す                    　　//
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool RayCheck()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray = RayforCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * f_RayDir, Color.red, 2, false);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, f_RayDir) && hit.collider == gameObject.GetComponent<Collider>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 関数名 CatchObject                                                                                                    //
    /// 引数:void                                                                                                             //
    /// 返り値:void                                                                                                           //
    /// 機能：カメラの中心からレイを飛ばし、そのレイの距離内に掴めるオブジェクトがあった場合オブジェクト座標を2D座標にする　　//
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void CatchObject()
    {
        Debug.Log("RayIN");
        if (RayCheck())
        {
            if (MSystem.CatchObject == null)
            {
                MSystem.CatchObject = gameObject;
            }
            b_IsCatch = true;
        }
        else
        {
            b_IsCatch = false;
        }

    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 関数名 MovePosition                                                                                                   //
    /// 引数:void                                                                                                             //
    /// 返り値:void                                                                                                           //
    /// 機能：レイの距離管理、掴んだオブジェクトを2D座標に追わせる                                                            //
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void MovePosition()
    {
        f_scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 mousePos = Input.mousePosition;

        Vector3 DefaultPos = Input.mousePosition;

        //Debug.Log(f_scroll);

        if (f_scroll > 0)
        {
            if (f_ObjectDir < 5.0f)
            {
                f_ObjectDir += 0.2f;
            }
        }
        if (f_scroll < 0)
        {
            if (f_ObjectDir > 0)
            {
                f_ObjectDir -= 0.2f;
            }
        }

        mousePos.z = f_ObjectDir;

        DefaultPos.z = 0.0f;

        v_moveTo = Camera.main.ScreenToWorldPoint(mousePos);

        v_savePos = Camera.main.ScreenToWorldPoint(DefaultPos);


     
        transform.position = v_savePos;
        transform.position = Vector3.Lerp(transform.position, v_moveTo, 0.7f);
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 関数名 MoveRotation                                                                                                   //
    /// 引数:void                                                                                                             //
    /// 返り値:void                                                                                                           //
    /// 機能：掴んだオブジェクトを回転                                                                                        //
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void MoveRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            b_rotflg = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            b_rotflg = false;
        }

        if (b_rotflg)
        {
            transform.Rotate(new Vector3(0, 0, 1));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            f_ObjectDir -= 0.2f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            f_ObjectDir -= 0.2f;
        }
    }

    public bool GetIsCatch() { return b_IsCatch; }
}

