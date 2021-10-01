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

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        float verticalKey = Input.GetAxis("Vertical");

        //ボタンを離すと止まる
        if (!moveing)
        {
            speed *= 0.90f;
            rb.velocity = new Vector2(speed.x, speed.y);
        }
        moveing = false;

        //Dキーで右向きに動く
        if (horizontalKey > 0)
        {
            moveing = true;
            speed.x += acceleration;
            rb.velocity = speed;
            Debug.Log("D");
        }
        //Aキーで左向きに動く
        if (horizontalKey < 0)
        {
            moveing = true;
            speed.x -= acceleration;
            rb.velocity = speed;
            Debug.Log("A");
        }
        //Sキーで下向きに動く
        if (verticalKey < 0)
        {
            moveing = true;
            speed.y -= acceleration;
            rb.velocity = speed;
            Debug.Log("S");
        }
        //Wキーで上向きに動く
        if (verticalKey > 0)
        {
            moveing = true;
            speed.y += acceleration;
            rb.velocity = speed;
            Debug.Log("W");
        }
    }
}
