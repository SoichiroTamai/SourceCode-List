using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isRaiseFinger : MonoBehaviour
{
    public OVRSkeleton _skeleton; //�E��A�������͍���� Bone���

    [HideInInspector] public bool isIndexStraight;
    [HideInInspector] public bool isMiddleStraight;
    [HideInInspector] public bool isRingStraight;
    [HideInInspector] public bool isPinkyStraight;

    /// <summary>
    /// �w�肵���S�Ă�BoneID��������ɂ��邩�ǂ������ׂ�
    /// </summary>
    /// <param name="threshold">臒l 1�ɋ߂��قǌ�����</param>
    /// <param name="boneids"></param>
    /// <returns></returns>
    private bool IsStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        if (boneids.Length < 3) return false;   //���ׂ悤���Ȃ�
        Vector3? oldVec = null;
        var dot = 1.0f;
        for (var index = 0; index < boneids.Length - 1; index++)
        {
            var v = (_skeleton.Bones[(int)boneids[index + 1]].Transform.position - _skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value); //���ς̒l�𑍏悵�Ă���
            }
            oldVec = v;//�ЂƂO�̎w�x�N�g��
        }
        return dot >= threshold; //�w�肵��BoneID�̓��ς̑��悪臒l�𒴂��Ă����璼���Ƃ݂Ȃ�
    }

    private void Update()
    {
        if(_skeleton == null) { return; }
        if(_skeleton.Bones == null) { return; }
        if(_skeleton.Bones.Count <= 0) { return; }

        isIndexStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
        isMiddleStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Middle1, OVRSkeleton.BoneId.Hand_Middle2, OVRSkeleton.BoneId.Hand_Middle3, OVRSkeleton.BoneId.Hand_MiddleTip);
        isRingStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        isPinkyStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);

        //string isFinger(bool isStraight)
        //{
        //    return isStraight ? "�܂�����":"�Ȃ����Ă�";
        //}

        //Debug.Log("�l�����w��" + isFinger(isIndexStraight));
        //Debug.Log("���w��" + isFinger(isMiddleStraight));
        //Debug.Log("��w��" + isFinger(isRingStraight));
        //Debug.Log("���w��" + isFinger(isPinkyStraight));

        //if (isIndexStraight && isMiddleStraight && !isRingStraight && !isPinkyStraight)
        //{ //�l�����w�ƒ��w�����܂������ŁA���̑����Ȃ����Ă���
        //    Debug.Log("�s�[�X�I");
        //}
    }
}
