/*
    [参考サイト] https://qiita.com/yoship1639/items/1d49d5b481f988da7142 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //シーン遷移させる場合に必要

public class MultifunctionFollowingCamera : MonoBehaviour
{
    //////////////////////////
    /// 変数宣言
    ////////////////////////

    // インスペクターで編集可能
    [SerializeField] public         Transform target;               // 注視点にするオブジェクト
    [SerializeField] public bool    enableInput         = true;     // カメラの操作権限 (true = 操作可能)
    [SerializeField] public bool    simulateFixedUpdate = false;    // FixedUpdate の切り替え (true = 一定秒数, false = 毎フレーム) 
    [SerializeField] public bool    enableDollyZoom     = true;     // ドリーズーム(true = ON)　※被写体をそのままのサイズで移しつつ、背景に引きをもたらす撮影方法    [SerializeField]
    [SerializeField] public bool    enableWallDetection = true;     // 壁検知(埋まり防止)　(true = 埋まらない)
    [SerializeField] public bool    enableFixedPoint    = false;    // 定点カメラの切り替え (true = 定点)
    [SerializeField] public float   inputSpeed          = 4.0f;     // 視点操作感度
    [SerializeField] public Vector3 freeLookRotation;               // フリーカメラ (任意の位置に移動可能)
    [SerializeField] public float   height   = 1.5f;                            // カメラの高さ (Y軸)  -
    [SerializeField] public float   distance = 5.0f;                            // targetとの距離       |→　カメラの位置を調整
    [SerializeField] public Vector3 rotation = new Vector3(10.0f, 0.0f, 0.0f);  // カメラの角度        -
    [SerializeField] [Range(0.01f, 100.0f)] public float positionDamping = 16.0f;   // カメラの遅延 (座標)
    [SerializeField] [Range(0.01f, 100.0f)] public float rotationDamping = 16.0f;   // 　　　〃     (角度)　近づく速度の調整可能
    [SerializeField] [Range(0.1f, 0.99f)]   public float dolly = 0.34f;             // dolly 効果量
    [SerializeField] public float       noise = 0.0f;                   // 手ブレ量
    [SerializeField] public float       noiseZ = 0.0f;                  // 前後のブレ
    [SerializeField] public float       noiseSpeed = 1.0f;              // ブレの速度
    [SerializeField] public Vector3     vibration = Vector3.zero;       // 振動 (カメラの揺れ)
    [SerializeField] public float       wallDetectionDistance = 0.3f;   // 壁との距離
    [SerializeField] public LayerMask   wallDetectionMask = 1;          // レイキャストの選択　※ 1 = TransparentFX(透明効果)　(0 = 全コライダーに当たらない )

    
    // コード内で使用
    private Camera  cam;                // カメラの情報格納用
    private float   targetDistance;     // targetとの距離
    private Vector3 targetPosition;     // 　〃　の座標
    private Vector3 targetRotation;     //　 〃　の角度
    private Vector3 targetFree;         // フリーカメラの角度
    private float   targetHeight;       // カメラの高さ
    private float   targetDolly;        // カメラの遅延


    void Awake()
    {
        // 自身のデータを引き続き
        DontDestroyOnLoad(this);
    }

    ///////////////////////////////////////
    /// 初期化
    //////////////////////////////////////

    void Start()
    {

        // カメラの情報を取得
        cam = GetComponent<Camera>();

        // target オブジェクトに関する情報を取得
        targetDistance = distance;          // 距離
        targetRotation = rotation;          // カメラの角度
        targetFree     = freeLookRotation;  // フリーカメラの角度
        targetHeight   = height;            // 高さ
        targetDolly    = dolly;             // 遅延

        // 遅延分の距離を格納
        var dollyDist = targetDistance;

        // ドリーズームが ON なら
        if (enableDollyZoom)
        {
            // 指定した距離で指定錐台の高さを得るために必要な FOV を計算
            var dollyFoV = GetDollyFoV(Mathf.Pow(1.0f / targetDolly, 2.0f), targetDistance);
            /// 遅延差分を取得
            dollyDist = GetDollyDistance(dollyFoV, targetDistance);
            // カメラの視野角に代入
            cam.fieldOfView = dollyFoV;
        }

        // targetが無い時は以降の処理をしない
        if (target == null) return;

        // 計算用に target オブジェクトを元に座標を保持
        var pos = target.position + Vector3.up * targetHeight;

        // カメラの座標を初期化 → target を中心にカメラを指定の位置へ移動　※水平回転角度 * 上下回り込み
        var offset = Vector3.zero;
        offset.x += Mathf.Sin(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.z += -Mathf.Cos(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.y += Mathf.Sin(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        targetPosition = pos + offset;
    }

    ///////////////////////////////////////
    /// 更新 (毎フレーム)
    //////////////////////////////////////
    
    void Update()
    {
        // deltaTime → 直前のフレームと今のフレーム間で経過した時間[秒] を返す
        if (!simulateFixedUpdate) Simulate(Time.deltaTime);
    }

    ///////////////////////////////////////
    /// 更新 (一定秒数) ※ 「Edit」→「Project Setting」→「Time」→「Fixed Timestep」にて間隔の変更可能
    //////////////////////////////////////

    void FixedUpdate()
    {
        // fixedDeltaTime → 通常時は最後のフレームからの経過時間だが、FixedUpdate内で呼ばれる場合のみ、ゲーム内時間を進める秒数に仕様が変わる
        // ※処理落ちして、最後のフレームからの経過時間がMaximum Allowed Timestep(上限)を超えた場合、実際の経過時間ではなくMaximum Allowed Timestepと同じ値
        if (simulateFixedUpdate) Simulate(Time.fixedDeltaTime);
    }

    ///////////////////////////////////////
    /// 更新処理
    ///////////////////////////////////////

    private void Simulate(float deltaTime)
    {
        // カメラは操作可能？
        if (enableInput)
        {
            // カメラのtargetとの距離を変更
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                dolly += Input.GetAxis("Mouse ScrollWheel") * 0.2f;
                dolly = Mathf.Clamp(dolly, 0.1f, 0.99f);
            }
            else
            {
                distance *= 1.0f - Input.GetAxis("Mouse ScrollWheel");
                distance = Mathf.Clamp(distance, 0.01f, 1000.0f);
            }
            // targetを中心にカメラを回転移動(左クリック)
            if (Input.GetMouseButton(0))
            {
                rotation.x -= Input.GetAxis("Mouse Y") * inputSpeed;
                rotation.x = Mathf.Clamp(rotation.x, -89.9f, 89.9f);
                rotation.y -= Input.GetAxis("Mouse X") * inputSpeed;
            }
            // カメラの角度を変更(右クリック)
            if (Input.GetMouseButton(1))
            {
                freeLookRotation.x -= Input.GetAxis("Mouse Y") * inputSpeed * 0.2f;
                freeLookRotation.y += Input.GetAxis("Mouse X") * inputSpeed * 0.2f;
            }
            // カメラの角度を初期化
            if (Input.GetMouseButtonDown(2))
            {
                freeLookRotation = Vector3.zero;
            }
        }

        // 遅延処理用の変数を宣言
        // Mathf.Lerpのrate値を時間にしてあげる事で、時間がたつにつれてtargetRotはrotに近づくようになる
        // rotationDamping → 近づく速度

        var posDampRate = Mathf.Clamp01(deltaTime * 100.0f / positionDamping);  // 座標
        var rotDampRate = Mathf.Clamp01(deltaTime * 100.0f / rotationDamping);  // 角度

        targetDistance = Mathf.Lerp(targetDistance, distance, posDampRate);
        targetRotation = Vector3.Lerp(targetRotation, rotation, rotDampRate);
        targetFree     = Vector3.Lerp(targetFree, freeLookRotation, rotDampRate);
        targetHeight   = Mathf.Lerp(targetHeight, height, posDampRate);
        targetDolly    = Mathf.Lerp(targetDolly, dolly, posDampRate);


        // ターゲットとの差を詰める
        if (Mathf.Abs(targetDolly - dolly) > 0.005f)
        {
            targetDistance = distance;
        }

        // ドリーズームの距離
        var dollyDist = targetDistance;

        // ドリーズームは有効？
        if (enableDollyZoom)
        {
            // 錘台の高さを取得
            var dollyFoV = GetDollyFoV(Mathf.Pow(1.0f / targetDolly, 2.0f), targetDistance);
            dollyDist = GetDollyDistance(dollyFoV, targetDistance);
            cam.fieldOfView = dollyFoV;     // 新しい画角を格納
        }

        // targetがなければ処理は行わない
        if (target == null) return;

        // カメラの位置を調整 (ターゲットを中心にして回る + 高さ) 
        var pos = target.position + Vector3.up * targetHeight;

        // カメラの壁埋まり防止が有効か？
        if (enableWallDetection)
        {
            // レイ情報を作成
            RaycastHit hit;
            
            // targetとの差分を正規化 
            var dir = (targetPosition - pos).normalized;

            // 任意のコライダーとのレイの当たり判定 (true = 当たっている) ※ コライダーはレイヤー別に選択可能
            if (Physics.SphereCast(pos, wallDetectionDistance, dir, out hit, dollyDist, wallDetectionMask))
            {
                // 埋まった分を差し戻す
                dollyDist = hit.distance;
            }
        }

        // カメラオフセット初期化 + 再設定
        var offset = Vector3.zero;  // 水平回転角度              *                上下回り込み角度             * カメラ距離
        offset.x +=  Mathf.Sin(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.z += -Mathf.Cos(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.y +=  Mathf.Sin(targetRotation.x * Mathf.Deg2Rad) * dollyDist;


        // ターゲットとの差を詰める
        if (Mathf.Abs(targetDolly - dolly) > 0.005f)
        {
            // 原点 + ローカル座標
            targetPosition = offset + pos;
        }
        else
        {
            // 徐々に詰める
            targetPosition = Vector3.Lerp(targetPosition, offset + pos, posDampRate);
        }

        // 定点カメラではない時 → 追従カメラ
        if (!enableFixedPoint) cam.transform.position = targetPosition;    // 座標
        cam.transform.LookAt(pos, Quaternion.Euler(0.0f, 0.0f, targetRotation.z) * Vector3.up); // 画角
        cam.transform.Rotate(targetFree);   // カメラ

        // 手ブレ(パーリンノイズ) ※ 緩やか
        // ※ Zだけ別の変数にしているのは、cameraのupの変動が大きいと見ずらくなってしまうので個別に調整できるようにするため
        if (noise > 0.0f || noiseZ > 0.0f)
        {
            // ノイズデータ作成
            var rotNoise = Vector3.zero;
            rotNoise.x = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0.0f) - 0.5f) * noise;
            rotNoise.y = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0.4f) - 0.5f) * noise;
            rotNoise.z = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0.8f) - 0.5f) * noiseZ;
            cam.transform.Rotate(rotNoise);
        }

        // 振動はあるか？　※ 激しい　.sqrMagnitude → ベクトルの2条の長さで返す
        if (vibration.sqrMagnitude > 0.0f)
        {
            // ランダムに回転量を追加
            cam.transform.Rotate(new Vector3(Random.Range(-1.0f, 1.0f) * vibration.x, Random.Range(-1.0f, 1.0f) * vibration.y, Random.Range(-1.0f, 1.0f) * vibration.z));
        }
    }

    // 遅延差分を取得し Dolly Zoom 効果を開始
    private float GetDollyDistance(float fov, float distance)
    {
        return distance / (2.0f * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad));
    }

    // カメラから指定の距離にある錐台の高さを計算
    private float GetFrustomHeight(float distance, float fov)
    {
        return 2.0f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
    }

    // 指定した距離で指定の錐台の高さを得るために必要な FOV を計算
    private float GetDollyFoV(float dolly, float distance)
    {
        return 2.0f * Mathf.Atan(distance * 0.5f / dolly) * Mathf.Rad2Deg;
    }
}
