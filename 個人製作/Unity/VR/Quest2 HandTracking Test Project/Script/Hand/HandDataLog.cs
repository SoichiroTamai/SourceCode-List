/*
 Pinch　 … 低精度
 Straight … 高精度
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class HandDataLog : MonoBehaviour
{
    [SerializeField] private OVRHand hand;

    [SerializeField] private isRaiseFinger isRF;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();

        text.text =
            "Hand_PinchData\n" +
            "\nThumb  : " +
            "\nIndex  : " +
            "\nMiddle : " + 
            "\nRing   : " + 
            "\nPinky  : " ;
    }

    // Update is called once per frame
    void Update()
    {
        if(hand == null) { return; }
        if(isRF == null) { return; }

        text.text =
            "Hand_PinchData\n" +
            "\nThumb  : " + hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) +
            "\nIndex  : " + isRF.isIndexStraight  + ":" + hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) +
            "\nMiddle : " + isRF.isMiddleStraight + ":" + hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) +
            "\nRing   : " + isRF.isRingStraight   + ":" + hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) +
            "\nPinky  : " + isRF.isPinkyStraight  + ":" + hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

    }
}
