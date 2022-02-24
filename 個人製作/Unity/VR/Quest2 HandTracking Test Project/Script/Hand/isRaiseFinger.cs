using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isRaiseFinger : MonoBehaviour
{
    public OVRSkeleton _skeleton; //右手、もしくは左手の Bone情報

    [HideInInspector] public bool isIndexStraight;
    [HideInInspector] public bool isMiddleStraight;
    [HideInInspector] public bool isRingStraight;
    [HideInInspector] public bool isPinkyStraight;

    /// <summary>
    /// 指定した全てのBoneIDが直線状にあるかどうか調べる
    /// </summary>
    /// <param name="threshold">閾値 1に近いほど厳しい</param>
    /// <param name="boneids"></param>
    /// <returns></returns>
    private bool IsStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        if (boneids.Length < 3) return false;   //調べようがない
        Vector3? oldVec = null;
        var dot = 1.0f;
        for (var index = 0; index < boneids.Length - 1; index++)
        {
            var v = (_skeleton.Bones[(int)boneids[index + 1]].Transform.position - _skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value); //内積の値を総乗していく
            }
            oldVec = v;//ひとつ前の指ベクトル
        }
        return dot >= threshold; //指定したBoneIDの内積の総乗が閾値を超えていたら直線とみなす
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
        //    return isStraight ? "まっすぐ":"曲がってる";
        //}

        //Debug.Log("人差し指は" + isFinger(isIndexStraight));
        //Debug.Log("中指は" + isFinger(isMiddleStraight));
        //Debug.Log("薬指は" + isFinger(isRingStraight));
        //Debug.Log("小指は" + isFinger(isPinkyStraight));

        //if (isIndexStraight && isMiddleStraight && !isRingStraight && !isPinkyStraight)
        //{ //人差し指と中指だけまっすぐで、その他が曲がっている
        //    Debug.Log("ピース！");
        //}
    }
}
