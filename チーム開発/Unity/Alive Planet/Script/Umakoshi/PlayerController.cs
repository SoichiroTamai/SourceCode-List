using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 speed;

    public float acceleration;    
    private bool moveing=false ;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = 0.1f;
    }

    // �������Z���������ꍇ��FixedUpdate���g���̂���ʓI
    void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        float verticalKey = Input.GetAxis("Vertical");

        //�{�^���𗣂��Ǝ~�܂�
        if (!moveing)
        {
            speed *= 0.90f;
            rb.velocity = new Vector2(speed.x, speed.y);
        }
        moveing = false;

        //D�L�[�ŉE�����ɓ���
        if (horizontalKey > 0)
        {
            moveing = true;
            speed.x += acceleration;
            rb.velocity = speed;
            Debug.Log("D");
        }
        //A�L�[�ō������ɓ���
        if (horizontalKey < 0)
        {
            moveing = true;
            speed.x -= acceleration;
            rb.velocity = speed;
            Debug.Log("A");
        }
        //S�L�[�ŉ������ɓ���
        if (verticalKey < 0)
        {
            moveing = true;
            speed.y -= acceleration;
            rb.velocity = speed;
            Debug.Log("S");
        }
        //W�L�[�ŏ�����ɓ���
        if (verticalKey > 0)
        {
            moveing = true;
            speed.y += acceleration;
            rb.velocity = speed;
            Debug.Log("W");
        }
    }
}
