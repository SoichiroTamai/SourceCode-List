using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;

    private Vector3 m_velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_speed = 0;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_speed = 5;
        }
    }
}
