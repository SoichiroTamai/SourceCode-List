using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool IS_CameraAnimation;

    public GameObject LookTarget;        //見るプレイヤー
    public GameObject PosTarget;
    public GameObject LookPlayer;

    public bool targetChange;

    public bool playerChange;

    public bool SetCamera;

    public float rotateSpeed = 3.0f; // 回転させるスピード

    private Vector3 v_mePotision;   // オブジェクトのもともとの位置

    private Quaternion q_meQuaternion;

    private float f_backTime;

    private float f_backPos;

    private bool b_SetPos;

    // Start is called before the first frame update
    void Start()
    {
        v_mePotision = LookPlayer.transform.position;
        q_meQuaternion = LookPlayer.transform.rotation;
        IS_CameraAnimation = false;
        targetChange = false;
        playerChange = false;
        SetCamera = false;
        b_SetPos = false;
        f_backPos = 1;
        f_backTime = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            SetCamera = true;
        }

        if (SetCamera)
        {
            if (!b_SetPos)
            {
                v_mePotision = LookPlayer.transform.position;
                q_meQuaternion = LookPlayer.transform.rotation;
                b_SetPos = true;
            }
            targetChange = true;
            SetCamera = false;
        }


        if (f_backTime < 0)
        {
            playerChange = true;
        }

       

        if (targetChange)
        {
            if (!LookTarget) { return; }
            if (!LookPlayer) { return; }

            IS_CameraAnimation = true;
            LookPlayer.transform.LookAt(Vector3.Lerp(LookPlayer.transform.forward + LookPlayer.transform.position, LookTarget.transform.position, 0.02f), Vector3.up);
            LookPlayer.transform.position = Vector3.Lerp(LookPlayer.transform.position, PosTarget.transform.position, 0.04f);

            f_backTime -= Time.deltaTime;
        }

        if (playerChange)
        {
            targetChange = false;
            LookPlayer.transform.position = Vector3.Lerp(LookPlayer.transform.position, v_mePotision, 0.04f);

            f_backPos -= Time.deltaTime;

            LookPlayer.transform.rotation = q_meQuaternion;
            if (f_backPos < 0)
            {
                LookPlayer.transform.position = v_mePotision;
            }

            if (LookPlayer.transform.position == v_mePotision)
            {
                playerChange = false;
                f_backTime = 2;
                IS_CameraAnimation = false;
                b_SetPos = false;
                SetCamera = false;
            }
        }
    }
}