using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// isRF.is … true → 指を立てている

public class HandSign : MonoBehaviour
{
    [SerializeField] OVRHand leftHand;

    [HideInInspector] public isRaiseFinger isRF;

    private void Awake()
    {
        isRF = GetComponent<isRaiseFinger>();
    }

    // 人差し指 & 中指 (チョキ)
    public bool deuces
    {
        get
        {
            return (
                isRF.isIndexStraight && isRF.isMiddleStraight &&
                !isRF.isRingStraight && !isRF.isPinkyStraight
            );
        }
    }

    // グー (全て閉じている)
    public bool guu
    {
        get
        {
            return (
                !isRF.isIndexStraight && !isRF.isMiddleStraight &&
                !isRF.isRingStraight && !isRF.isPinkyStraight
            );
        }
    }

    // パー (全て閉じている)
    public bool Par
    {
        get
        {
            return (
                isRF.isIndexStraight && isRF.isMiddleStraight &&
                isRF.isRingStraight && isRF.isPinkyStraight
            );
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (deuces)
        {
            Debug.Log("Hand sign：deuces");
        }
    }
}
