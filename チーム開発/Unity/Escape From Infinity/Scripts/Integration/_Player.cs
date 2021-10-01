using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Player : MonoBehaviour
{
    // ¬¼ •Ï”//////////////////////////////////////
    public bool is_Grab;    //‚Â‚©‚İ

    ///////////////////////////////////

    // ¼‰Y •Ï”//////////////////////////////////////
    private float inputHorizontal;
    private float inputVertical;
    private Rigidbody rb;
    public GameObject camerapos;
    [SerializeField] private Vector3 velocity;              // ˆÚ“®•ûŒü
    [SerializeField] private float moveSpeed = 5.0f;

    //////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // ¼‰Y‰Šú‰» ///////////////////////
        rb = GetComponent<Rigidbody>();
        /////////////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        // ¼‰Y XV///////////////////////////////////////////////////////////////////////////////////////////
        ///ƒJƒƒ‰‚Ì³–Ê‚ÉŒü‚©‚Á‚ÄˆÚ“®
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

    // “–‚½‚è”»’è’†////////////////////////////////////
    private void OnCollisionStay(Collision collision)
    {
        // ¬¼///////////////////////////////
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

    // “–‚½‚è”»’è‚©‚ç—£‚ê‚½////////////////////////////
    private void OnCollisionExit(Collision collision)
    {
        // ¬¼///////////////////////////////////
        // •¨‘Ì‚ª—£‚ê‚½‚Æ‚«A‚P“x‚¾‚¯ŒÄ‚Î‚ê‚é
        is_Grab = false;
        /////////////////////////////////////////
    }
}
