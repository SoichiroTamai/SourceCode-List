using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal; //Light2Dを使うのに必要
using Drone;

// 現在の探索状況
public enum SearchStatus
{
    placement,  // ドローン配置
    search      // 探索中
}

public class MultiDatas : MonoBehaviour
{
    // 検索用
    public string findName { get {return "Malti Datas"; }}

    //キーコード
    [Header("操作")]
    [SerializeField] private KeyCode Up;
    [SerializeField] private KeyCode Down;
    [SerializeField] private KeyCode Left;
    [SerializeField] private KeyCode Right;

    private bool isShift; // Shiftキーが押されているか (ズームに使用)  

    [Header("GmaeMaster Class")]
    public GameMasterScript gameMasterScript;

    // カメラ
    [Header("カメラ")]
                      public GameObject               mainCamObj;         // メインカメラ
    [HideInInspector] public Camera                   mainCamera;         // メインカメラ情報
                      public GameObject               cmVCam;             // 追従するカメラ
    [HideInInspector] public CinemachineVirtualCamera virtualCamera;      // 追従するカメラ情報
    [HideInInspector] public GameObject               cameraFollowObject; // カメラの追従対象

    private Transform cameraTf;  // カメラ座標
    private bool isTargetCamera; // 追従カメラかどうか true=追従カメラ, false=手動操作

    [SerializeField, Range(0f, 0.2f)]
    private float cameraMoveSpeed; // 手動操作時のカメラ感度

    // ライト
    [Header("ライト")]
    public Light2D globalLight2DObject; // グローバルライト(画面全体)
    public Light2D pointLight2DObject;  // ポイントライト　(画面中心)
    //[SerializeField] private float   llght2DIntensity;       // 画面全体の明るさ

    // スクリーン
    [Header("スクリーン")]
    [SerializeField]  private TouchMonitorScript touchMonitorMonitor;
    [HideInInspector] public  SearchStatus       searchStatus;   // 現在の探索状況

    // ドローン
    [Header("ドローン")]
    [SerializeField] private DroneManager droneManager;
    public DroneManager GetDroneManager { get { return droneManager; } }
    public DroneSelect droneSelect;  // 所持しているドローンのドロップダウンボックス

    [SerializeField] private List<DroneData> droneDatas;   // ドローンリスト情報格納用
    public List<DroneData> GetDroneDatas { get { return droneDatas; } }

    // 回収予定の資源
    public List<GameObject> target_Resources; 

    // クリックした GameObject を取得
    public GameObject clickObject
    {
        get { return touchMonitorMonitor.clickObject; }
        set { touchMonitorMonitor.clickObject = value; }
    }

    // inspector欄の初期化時などに呼ばれる
    private void Reset()
    {
        Up = KeyCode.W;
        Down = KeyCode.S;
        Left = KeyCode.A;
        Right = KeyCode.D;

        cameraMoveSpeed = 0.1f;
    }

    // Start is called before the first frame update
    void Awake()
    {
        // 探索状況の初期化
        searchStatus = SearchStatus.placement;

        // クリック選択したオブジェクトを初期化
        clickObject = null;

        // 追従カメラの情報を取得
        mainCamera    = mainCamObj.GetComponent<Camera>();               // コンポーネント取得
        virtualCamera = cmVCam.GetComponent<CinemachineVirtualCamera>(); // コンポーネント取得

        isTargetCamera = false;

        // カメラ情報を取得
        cameraTf = virtualCamera.GetComponent<Transform>();

        droneDatas = new List<DroneData>();

        // ゲームマネージャーにドローンがあれば取得する
        try
        {
            droneDatas = gameMasterScript.GetDroneDataList();
        }
        catch
        {
            Debug.Log("GameMasterにドローンがありませんでした。");
        }

        // 回収予定のリソース
        target_Resources = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Update_Camera();

        if (!clickObject) { return; }

        // クリックしたオブジェクトがリソースだったら回収予定に追加
        if(clickObject.tag == "Resource")
        {
            if (target_Resources.Count <= 0)
            {
                target_Resources.Add(clickObject);
            }
            else
            {
                // 既に回収予定済みのリソースかどうか
                bool sameRes = false;

                foreach (var res in target_Resources)
                {
                    if(res == clickObject)
                    {
                        sameRes = true;
                    }
                }

                // 回収予定でなければ追加
                if (!sameRes)
                {
                    target_Resources.Add(clickObject);
                }
            }
        }
    }

    // カメラ更新
    void Update_Camera()
    {
        // フリーカメラ(WASD移動)
        if (searchStatus == SearchStatus.placement || !isTargetCamera)
        {
            // ズームに使用
            isShift = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift);

            // カメラ操作
            if (isShift)
            {
                float fov = virtualCamera.m_Lens.OrthographicSize;

                if (fov >= 3.0f)
                {
                    if (Input.GetKey(Up)) { virtualCamera.m_Lens.OrthographicSize = fov - cameraMoveSpeed; }
                }
                if (fov <= 5.3f)
                {
                    if (Input.GetKey(Down)) { virtualCamera.m_Lens.OrthographicSize = fov + cameraMoveSpeed; }
                }
            }
            else
            {
                if (Input.GetKey(Up))    { cameraTf.position = cameraTf.position + new Vector3(0.0f, cameraMoveSpeed, 0.0f); }
                if (Input.GetKey(Down))  { cameraTf.position = cameraTf.position - new Vector3(0.0f, cameraMoveSpeed, 0.0f); }
                if (Input.GetKey(Left))  { cameraTf.position = cameraTf.position - new Vector3(cameraMoveSpeed, 0.0f, 0.0f); }
                if (Input.GetKey(Right)) { cameraTf.position = cameraTf.position + new Vector3(cameraMoveSpeed, 0.0f, 0.0f); }
            }
        }
    }
}
