using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormScript : MonoBehaviour
{
    // ˆÚ“®—p
    [Header("ˆÚ“®")]
    public iTween.EaseType movementEaseType = iTween.EaseType.easeInOutSine;
    public float m_movementRadius;
    public float m_movementSpeed;

    private Vector3 originalPostion;

    // ‰~‰^“®ŽüŠú
    //[SerializeField] private float m_period;

    [Header("–Ú(‘ä•—)")]
    public Transform        m_pullingCenter;
    public AnimationCurve   m_pullingCenterCurve;
    public float            m_pullRadius;
    
    [Header("‹­‚³")]
    public float            m_pullForce;
    public float            m_pullForcem_magnification;
    public AnimationCurve   m_pullForceCurve;

    private void Reset()
    {
        m_movementSpeed = 0.5f;
        //m_period = 2f;
        m_pullForcem_magnification = 1.0f;

    }

    // Start is called before the first frame update
    void Start()
    {
        originalPostion = transform.position;
        StartCoroutine(Movement());
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        StartCoroutine(IncreasePull(other, true));
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Player")
        StartCoroutine(IncreasePull(other, false));
    }

    IEnumerator Movement()
    {
        // chaoose location
        var newPos  = new Vector3(originalPostion.x + Random.Range(-m_movementRadius, m_movementRadius), originalPostion.y, originalPostion.z + Random.Range(-m_movementRadius, m_movementRadius));
        var distans = newPos - originalPostion;
        var time = distans.magnitude / m_movementSpeed;

        // move to location
        iTween.MoveTo(gameObject, iTween.Hash("position", newPos, "easeType", movementEaseType, "time", time));
        yield return new WaitForSeconds(time + 0.1f);
        StartCoroutine(Movement());
    }

    IEnumerator IncreasePull(Collider other, bool pull)
    {
        if (pull)
        {
            if (other.GetComponent<Rigidbody>())
            {
                // pull force controlled by curve
                m_pullForce = m_pullForceCurve.Evaluate(((Time.time * 0.1f) % m_pullForceCurve.length)) * m_pullForcem_magnification;

                // get direction form tornado to object
                Vector3 forceDirection = m_pullingCenter.position - other.transform.position;
                if (forceDirection.y <= 0.0f)
                {
                    forceDirection.y = 0.01f;
                }
                //else
                //{
                //    forceDirection.y *= 2.0f;
                //}

                // apply foce to object towards tornado
                Vector3 foce = forceDirection * m_pullForce * Time.deltaTime;
                //Vector3 foce = new Vector3(0.0f, 5.0f,0.0f);

                //Debug.Log("forceDirection.normalized:" + forceDirection);
                //Debug.Log("m_pullForce:" + m_pullForce);
                //Debug.Log("Time.deltaTime:" + Time.deltaTime);
                //Debug.Log("foce:" + foce);

                other.GetComponent<Rigidbody>().AddForce(foce);
                //other.transform.position += foce;

                //Time.timeScale = 1.0f;

                float curveTime = (Time.time - ( Mathf.FloorToInt(Time.time / 10.0f) * 10)) * 0.1f;

                float posY = m_pullingCenterCurve.Evaluate(curveTime % m_pullingCenterCurve.length) * 10.0f;
                //Debug.Log("m_pullingCenterCurve.length:" + m_pullingCenterCurve.length);
                //Debug.Log("posY:" + posY);

                m_pullingCenter.position = new Vector3(m_pullingCenter.position.x, posY, m_pullingCenter.position.z);

                m_pullingCenter.RotateAround(
                    transform.position,
                    Vector3.up,
                    0.1f    // (360 / m_period * Time.deltaTime)
                );

                //Debug.Log("Time.deltaTime:" + Time.deltaTime);
                //Debug.Log("Rot:" + (360 / m_period * Time.deltaTime));

                //yield return refreshRate;
                yield return null;
                StartCoroutine(IncreasePull(other, pull));
            }
        }
    }
}