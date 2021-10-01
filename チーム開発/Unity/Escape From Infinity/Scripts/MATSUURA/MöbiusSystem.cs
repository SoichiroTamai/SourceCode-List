using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;           // 配列コピー用
using System.Linq;      // 配列比較用

public class MöbiusSystem : MonoBehaviour
{
    // 操作キー設定
    [SerializeField]
    public KeyCode key_Record = KeyCode.R;  // 記録キー
    [SerializeField]
    public KeyCode key_Play = KeyCode.Space;  // 再生キー
    [SerializeField]
    public KeyCode key_Reset = KeyCode.B;  // リセットキー

    // システムデータ
    private List<Vector3> l_GameObj_Pos = new List<Vector3>();       // 記憶媒体(List型 Pos)
    private List<Quaternion> l_GameObj_Rot = new List<Quaternion>();    // 記憶媒体(List型 Rot)
    private List<Vector3> l_GameObj_Scale = new List<Vector3>();       // 記憶媒体(List型 Scale)

    private bool Repetition;                // 往復フラグ(false:逆進行 true:正規進行)

    private int CountFrame;                 // 収録フレーム総数カウンター

    private int Num_FramesRecorded;         // 収録したフレーム数

    private Quaternion q_DefRot;            // オブジェクトの回転量

    [SerializeField]
    private float RecordableTime = 10f;   // 収録可能な最大秒数

    // 外部参照
    private Effekseer.EffekseerEmitter EE_effekSeerEmi;   // エフェクト変数(EffekSeer)

    private GameObject Rec_Particle_Obj;     // 記録した動きを可視化するパーティクルの座標管理用

    [SerializeField]
    private ParticleSystem[] Recorded_Particle;     // 記録した動きを可視化するパーティクル
    private ParticleSystem[] OldRecorded_Particle;  // 1フレーム前に使用していたパーティクル


    private bool Section;                   // 動かせるフラグ

    private bool RecordedTrail;             // 記録した軌跡を動かせるフラグ

    [HideInInspector]
    public bool Destory;                    // 記憶した動きを破壊するフラグ

    [HideInInspector]
    public bool Stop;                       // 動きの停止フラグ

    [HideInInspector]
    public ScoreManeger scoreManeger;   // スコアに必要な情報をやり取りする用 ( 使用回数等 )

    //サウンド
    AudioSource effectSE;
    public AudioClip recordSE;
    public AudioClip executionSE;

    [HideInInspector]
    public float seconds;                  // 収録可能な残り時間

    public GameObject MöbiusSprite;

    public GameObject CatchObject;          // 対象オブジェクト変数

    // システム管理フラグ
    //[HideInInspector]
    public bool Recood;                    // 収録するフラグ

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        scoreManeger = GameObject.Find("ScoreManeger").GetComponent<ScoreManeger>();
        if (!scoreManeger) { Debug.LogError("MöbiusSystem.cs　Start() にて ScoreManeger 取得失敗しました。"); }

        // エフェクト初期化
        Effect_Initialize();

        // メビウスシステム初期化
        möbiusSystem_Reset();

        //サウンドコンポーネント取得
        effectSE = this.GetComponent<AudioSource>();

    }

    // メビウスシステムの情報を初期化
    void möbiusSystem_Reset()
    {
        Section = false;
        Recood = false;
        Repetition = false;
        Destory = false;
        Stop = false;
        seconds = RecordableTime;
        q_DefRot = MöbiusSprite.transform.rotation;
        Num_FramesRecorded = 0;

        // 記録軌跡停止
        foreach (var rec_ps in Recorded_Particle)
        {
            rec_ps.Stop();
            //rec_ps.Clear();
        }

        RecordedTrail = false;
    }

    // エフェクトの初期化
    void Effect_Initialize()
    {
        // 記録した動きを可視化するパーティクルの座標管理用
        Rec_Particle_Obj = GameObject.Find("Recorded Particle");
        if (!Rec_Particle_Obj) { Debug.Log("Rec_Particle_Obj 取得失敗"); }

        if (Recorded_Particle.Length <= 0)
        {
            Debug.LogWarning("Recorded_Particl を 設定していません。" +
                             "設定する場合は MöbiusSystem のインスペクター内にある Recorded_Particl にて修正をお願いします。");
            // Array.Resize(ref Recorded_Particle, 1);
            //Recorded_Particle[0] = Rec_Particle_Obj.transform.Find("Recorded Particle_Box").GetComponent<ParticleSystem>();
        }

        // 比較用配列に値をコピー ※ = の場合アドレスがコピーされる
        Array.Resize(ref OldRecorded_Particle, Recorded_Particle.Length);
        Recorded_Particle.CopyTo(OldRecorded_Particle, 0);

        // エフェクシア初期化
        EE_effekSeerEmi = GetComponent<Effekseer.EffekseerEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        // エフェクトの変更があれば修正する
        Change_RecordedParticle();

        // 座標記録開始
        if (Input.GetKeyDown(key_Record) && !Recood && Num_FramesRecorded == 0)
        {
            if (!CatchObject) { return; }

            Recood = true;
            Debug.Log("記録開始");

            // システム使用回数を加算
            if (scoreManeger)
            {
                scoreManeger.AddTryNum();
            }

            effectSE.PlayOneShot(recordSE);
        }

        // システム起動(記録した座標を移動)
        if (Input.GetKeyDown(key_Play) && !Section)
        {
            if (!CatchObject) { return; }

            if (CatchObject == GameObject.Find("Cube"))
            {
                Debug.Log("IN");
            }

            Section = true;

            // 記録中に押しても終了する
            RecodEnd();

            MöbiusSprite.transform.rotation = q_DefRot;

            Debug.Log("システム起動");
        }

        // システムリセット
        if (Input.GetKeyDown(key_Reset) && !Destory && Num_FramesRecorded != 0)
        {
            if (!CatchObject) { return; }

            Destory = true;
            Debug.Log("メビウスシステムリセット");
            return;
        }

        Recoding();
        Moving();
        RecetList();
    }

    // 動き（Transform）を記憶する関数
    private void Recoding()
    {
        if (!CatchObject) { return; }
        if (!Recood) { return; }

        if (MöbiusSprite)
        {
            MöbiusSprite.transform.Rotate(new Vector3(0, 0.2f, 0));
        }

        Vector3 pos = CatchObject.transform.position;
        Quaternion Rot = CatchObject.transform.rotation;
        Vector3 Scale = CatchObject.transform.localScale;

        l_GameObj_Pos.Add(pos);
        l_GameObj_Rot.Add(Rot);
        l_GameObj_Scale.Add(Scale);

        seconds -= Time.deltaTime;

        //Debug.Log("入ってる");

        //Debug.Log("記録可能時間" + seconds);

        // 記録時間終了
        if (seconds <= 0)
        {
            RecodEnd();
        }
    }

    // 記録終了
    private void RecodEnd()
    {
        Recood = false;
        CountFrame = l_GameObj_Pos.Count - 1;

        Num_FramesRecorded = CountFrame;

        Debug.Log("終わり");
        Debug.Log("CountFrame" + CountFrame);

        ObjectMove(0);

        // 記録途中なら
        if (Section)
        {
            //Debug.Log("停止");

            foreach (var rec_ps in Recorded_Particle)
            {
                rec_ps.Stop();
            }

            RecordedTrail = false;
        }
        else
        {
            //Debug.Log("再生");

            foreach (var rec_ps in Recorded_Particle)
            {
                rec_ps.Play();
            }

            RecordedTrail = true;
        }
    }

    // 記憶させた動きを往復させる関数
    private void Moving()
    {
        if (!CatchObject) { return; }
        if (Stop) { return; }

        if (!Section)
        {
            if (!RecordedTrail)
            {
                return;
            }
        }
        else
        {
            // 重力をOFF
            catchObject_useGravity(false);
        }

        if (!Repetition)
        {
            ObjectMove(CountFrame);

            CountFrame--;
            if (CountFrame <= 0)
            {
                CountFrame = 0;
                Repetition = true;
            }
        }
        else
        {
            ObjectMove(CountFrame);

            CountFrame++;

            if (CountFrame >= l_GameObj_Pos.Count)
            {
                CountFrame = l_GameObj_Pos.Count - 1;
                Repetition = false;
            }
        }
    }

    // オブジェクトを保持した座標へ移動
    private void ObjectMove(int Frame)
    {
        GameObject moveObject = CatchObject;

        // 移動対象が軌跡の場合
        if (RecordedTrail)
        {
            moveObject = Rec_Particle_Obj;
        }

        // 実働中ならエフェクトを再生
        if (Section)
        {
            EE_effekSeerEmi.transform.position = moveObject.transform.position;
            if (!EE_effekSeerEmi.exists)
            {
                EE_effekSeerEmi.Play(EE_effekSeerEmi.effectAsset);
                effectSE.Play();
            }
        }

        Vector3 pos = moveObject.transform.position;
        Quaternion rot = moveObject.transform.rotation;
        Vector3 scale = moveObject.transform.localScale;

        pos = l_GameObj_Pos[Frame];
        rot = l_GameObj_Rot[Frame];
        scale = l_GameObj_Scale[Frame];

        moveObject.transform.position = pos;
        moveObject.transform.rotation = rot;

        if (!RecordedTrail)
        {
            moveObject.transform.localScale = scale;
        }
    }

    // 記憶したものを破壊する
    public void RecetList()
    {
        if (!Destory) { return; }
        if (l_GameObj_Pos.Count <= 0) { return; }

        Debug.Log("補完した情報をリセット");

        ObjectMove(CountFrame);

        l_GameObj_Pos.Clear();
        l_GameObj_Rot.Clear();
        l_GameObj_Scale.Clear();

        möbiusSystem_Reset();

        // 重力を戻す
        catchObject_useGravity(true);
    }

    // 記録の軌跡表示エフェクトの変更対応
    public void Change_RecordedParticle()
    {
        // 記録の軌跡表示が実行されていたら
        if (RecordedTrail)
        {
            // エフェクトが変更されたら ※配列内の比較
            if (!Recorded_Particle.SequenceEqual(OldRecorded_Particle))
            {
                Debug.Log("記録エフェクトの変更");

                // 前回起動していた軌跡を停止
                foreach (var rec_ps in OldRecorded_Particle)
                {
                    //Debug.Log("Old：" + rec_ps.name);
                    rec_ps.Stop();
                }

                // 新しく設定したエフェクトの再生を開始
                foreach (var rec_ps in Recorded_Particle)
                {
                    //Debug.Log("Now：" + rec_ps.name);
                    rec_ps.Play();
                }

                // 値をコピー
                Array.Resize(ref OldRecorded_Particle, Recorded_Particle.Length);
                Recorded_Particle.CopyTo(OldRecorded_Particle, 0);

                //Debug.Log("CatchObject : " + CatchObject);
                //Debug.Log("Stop : " + Stop);
            }
        }
    }

    // 掴んだオブジェクトに重力を適応するか
    // ※ 持っている間及びシステム使用中は重力を切る(下方向への重力加速度が累積され、離した時に初速度がついてしまうのを防止する為)
    public void catchObject_useGravity(bool useGravity)
    {
        if (!CatchObject) { return; }

        if (CatchObject.GetComponent<Rigidbody>().useGravity != useGravity)
        {
            CatchObject.GetComponent<Rigidbody>().useGravity = useGravity;
        }
    }

    // 収録したフレームを取得
    public int GetFramesRecorded()
    {
        return Num_FramesRecorded;
    }

    public Effekseer.EffekseerEmitter GetEffekseerEmitter()
    {
        return EE_effekSeerEmi;
    }
}