using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
　- 継承元の変数 -

    // 記録中かの判定用 ( true → 記録中) 
　　bool Recood;

    // 表示する座標　※ MöbiusSystem.cs で public に変更する必要がある [HideInInspector] も付与しインスペクター非表示に
    List<Vector3>    l_GameObj_Pos = new List<Vector3>();       // 記憶媒体(List型 Pos)
    
    // エフェクシアのエミュレーター
    Effekseer.EffekseerEmitter EE_effekSeerEmi;   // エフェクト変数(EffekSeer)


  - その他使用変数 -
　　EE_effekSeerEmi.exists                      … エフェクトがあるか
　  EE_effekSeerEmi.effectAsset                 … 再生したいエフェクト (エフェクシア)
    EE_effekSeerEmi.transform.position          … エフェクシアを再生する座標
    EE_effekSeerEmi.Play(再生したいエフェクト); … エフェクトの再生    


    Recording_Particle.Play()   // パーティクル再生
    Recording_Particle.Stop()   // パーティクル停止
    Recording_Particle.Clear()   // パーティクル停止
 */

public class MöbiusSystem_RecordingEffect : MöbiusSystem
{
    public MöbiusSystem mSystem;

    public Effekseer.EffekseerEffectAsset effectWindow;
    public Effekseer.EffekseerEffectAsset effectRecording;

    private bool effectFlg; // エフェクトが発生したらtrue

    // Start is called before the first frame update
    void Start()
    {
        effectFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // メビウスシステム記憶中か
        if(mSystem.Recood)
        {
            GameObject moveObject = mSystem.CatchObject;
            mSystem.GetEffekseerEmitter().isLooping = false;
            mSystem.GetEffekseerEmitter().transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            mSystem.GetEffekseerEmitter().transform.position = moveObject.transform.position;
            //エフェクトがあるか
            if (!mSystem.GetEffekseerEmitter().exists)
            {
                Debug.Log("エフェクト開始");
                mSystem.GetEffekseerEmitter().effectAsset = effectRecording;
                mSystem.GetEffekseerEmitter().Play(mSystem.GetEffekseerEmitter().effectAsset);
                effectFlg = true;
            }
        }
        else
        {
            if(effectFlg)
            {
                Debug.Log("エフェクト停止");
                mSystem.GetEffekseerEmitter().Stop();
                effectFlg = false;
                mSystem.GetEffekseerEmitter().effectAsset = effectWindow;
                mSystem.GetEffekseerEmitter().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }
}