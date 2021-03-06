using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Player : MonoBehaviour
{
    // 小西 変数//////////////////////////////////////
    public bool is_Grab;    //つかみ

    ///////////////////////////////////

    // 松浦 変数//////////////////////////////////////
    private float inputHorizontal;
    private float inputVertical;
    private Rigidbody rb;
    public GameObject camerapos;
    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;

    //////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // 松浦初期化 ///////////////////////
        rb = GetComponent<Rigidbody>();
        /////////////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        // 松浦 更新///////////////////////////////////////////////////////////////////////////////////////////
        ///カメラの正面に向かって移動
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = Vector3.Scale(camerapos.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * inputVertical + camerapos.transform.right * inputHorizontal;

        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    // 当たり判定中////////////////////////////////////
    private void OnCollisionStay(Collision collision)
    {
        // 小西///////////////////////////////
        if (Input.GetKey(KeyCode.E))
        {
            is_Grab = true;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            is_Grab = false;
        }
        //////////////////////////////////////
    }

    // 当たり判定から離れた時////////////////////////////
    private void OnCollisionExit(Collision collision)
    {
        // 小西///////////////////////////////////
        // 物体が離れたとき、１度だけ呼ばれる
        is_Grab = false;
        /////////////////////////////////////////
    }
}
