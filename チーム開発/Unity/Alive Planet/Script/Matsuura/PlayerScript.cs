using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Triger Up;
    public Triger Down;
    public Triger Right;
    public Triger Left;

    [SerializeField][RangeAttribute(0.0f,0.1f)]
    private float MoveSpeed = 0.025f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Right.triger)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(MoveSpeed, 0f, 0f);
            }
        }
        if (Left.triger)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-MoveSpeed, 0f, 0f);
            }
        }
        if (Up.triger)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0f, MoveSpeed, 0);
            }
        }
        if (Down.triger)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0f, -MoveSpeed, 0);
            }
        }

    }
}
