using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] OVRHand MYRightHand;
    [SerializeField] OVRSkeleton MYRightSkelton;
    [SerializeField] GameObject IndexSphere;

    [HideInInspector] public bool isIndexPinching;
    private float ThumbPinchStrength;

    void Update()
    {
        isIndexPinching = MYRightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        ThumbPinchStrength = MYRightHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);

        if (MYRightSkelton.Bones.Count <= 0) { return; }

        Vector3 indexTipPos = MYRightSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        Quaternion indexTipRotate = MYRightSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.rotation;
        IndexSphere.transform.position = indexTipPos;
        IndexSphere.transform.rotation = indexTipRotate;
    }

    void OnTriggerStay(Collider other)
    {
        // 掴んだ
        if (ThumbPinchStrength > 0.9)
        {
            other.gameObject.transform.parent = IndexSphere.transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.localPosition = Vector3.zero;

        }
        // 離した
        else
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.parent = null;

        }
    }
}