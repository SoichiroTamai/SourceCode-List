using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLerp : MonoBehaviour
{
    //開始位置と最終位置
    private Vector3 StartEmission;
    private Vector3 EndEmission;
    //移動にかける時間
    public int Duration;

    //ライトのオブジェクト
    public Light LightObject;
    public Color StartColor;
    public Color EndColor;

    //二点間の距離を入れる
    //現在の位置
    private float NowEmission = 0;

    //現在何秒経過しているか
    private float NowTime = 0;

    //どちら側に移動するか管理する変数
    private bool DoMove = false;

    // Start is called before the first frame update
    void Start()
    {
        StartEmission.x = 0;
        EndEmission.x = 1;
        LightObject = GetComponent<Light>();
    }

    // 更新
    void Update()
    {
        //進める
        if (DoMove)
        {
            //どれくらい時間が経過したのかを示す
            NowEmission = NowTime / Duration;

            //イージング関数により緩急を付ける
            NowEmission = SineInOut(NowEmission, Duration, StartEmission.x, EndEmission.x);

            //オブジェクトの移動
            Vector3 v = Vector3.Lerp(StartEmission, EndEmission, NowEmission);

            //ライトの色味も変更する
            LightObject.color = Color.Lerp(StartColor, EndColor, NowEmission);

            if (v.x >= 1)
            {
                InversionFlg();
            }
        }
        //戻す
        else
        {
            //どれくらい時間が経過したのかを示す
            NowEmission = NowTime / Duration;

            //イージング関数により緩急を付ける
            NowEmission = SineInOut(NowEmission, Duration, EndEmission.x, StartEmission.x);

            //オブジェクトの移動
            Vector3 v = Vector3.Lerp(EndEmission, StartEmission, NowEmission);

            LightObject.color = Color.Lerp(EndColor, StartColor, NowEmission);

            if (v.x <= 0)
            {
                InversionFlg();
            }
        }

        NowTime += Time.fixedDeltaTime;
    }

    public static float SineInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return -max / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

    private void InversionFlg()
    {
        DoMove = !DoMove;
    }
}
