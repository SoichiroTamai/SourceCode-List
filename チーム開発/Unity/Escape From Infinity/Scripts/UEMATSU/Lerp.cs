using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    //開始位置と最終位置
    public Transform StartPos;
    public Transform EndPos;
    //移動にかける時間
    public int Duration;

    //二点間の距離を入れる
    //現在の位置
    private float NowPos = 0;

    // Update is called once per frame
    void Update()
    {
        //どれくらい時間が経過したのかを示す
        NowPos = Time.time / Duration;

        //イージング関数により緩急を付ける
        NowPos = EaseOutBounce(NowPos);

        //オブジェクトの移動
        transform.position = Vector2.Lerp(StartPos.position, EndPos.position, NowPos);

        //今の位置にスピードを足して動かす
        //NowPos += speed;
    }

    private float EaseOutBounce(float t)
    {
        if (t < (1f / 2.75f))
        {
            return 7.5625f * t * t;
        }
        else if (t < (2f / 2.75f))
        {
            t -= (1.5f / 2.75f);
            return 7.5625f * (t) * t + .75f;
        }
        else if (t < (2.5f / 2.75))
        {
            t -= (2.25f / 2.75f);
            return 7.5625f * (t) * t + .9375f;
        }
        else
        {
            t -= (2.625f / 2.75f);
            return 7.5625f * (t) * t + .984375f;
        }
    }
}
