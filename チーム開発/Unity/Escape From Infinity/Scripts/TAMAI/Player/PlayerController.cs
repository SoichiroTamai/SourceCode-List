using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //シーン遷移させる場合に必要

public class PlayerController : Singleton<PlayerController>
{
    //////////////////////////
    /// 変数宣言
    ////////////////////////

    // インスペクターで編集可能
    [SerializeField] private float  jumpPower = 5.0f;    // ジャンプ力
    [SerializeField] private bool   isGrounded;          // 地面に接地しているかどうか
    [SerializeField] private float  walkSpeed = 1.5f;    // 歩く速さ
    [SerializeField] private float  turnSpeed = 5.0f;    // 回転の速さ
    [SerializeField] private int    layerMask;           // Playerレイヤー以外のレイヤーマスク

    // 空間移動
    private enum EpatialMovementPattern
    {
        Teleportation,      // 瞬間移動 　※ プレイヤーの座標を移動
        SceneMovement,      // シーン移動 ※ 別のシーンへ移動移動
        ObjectSwitching     // オブジェクトを切り替え  ※設置しているオブジェクトを変更
    }
    [SerializeField] private EpatialMovementPattern eatialMovementPattern;
    [SerializeField] private GameObject gateA;
    [SerializeField] private GameObject gateB;

    [SerializeField] private SceneObject sceneA;
    [SerializeField] private SceneObject sceneB;
    
    [SerializeField] private GameObject firstRoom;  // 最初に表示するオブジェクト    
    [SerializeField] private GameObject room1;      // 部屋1   
    [SerializeField] private GameObject room2;      // 部屋2


    // コード内で使用
    private Animator     animator;  // アニメーション
    private Vector3      input;     // 入力値
    private Vector3      velocity;  // ベクトル
    private Rigidbody    rigid;     // rigidbody (当たり判定等に使用)
    private GameObject[] players;   // プレイヤー
    private GameObject[] cameras;   // メインカメラ
    private GameObject[] room;      // 表示するオブジェクト
    private GameObject[] ActiveRoom;// 表示中のオブジェクト

    private Vector3 centor = new Vector3(0.0f, 0.8f, 0.0f);

    ///////////////////////////////////////
    /// 初期化 (引き続き等) ※ Start()の前に実行される
    //////////////////////////////////////

    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        // 自身のデータを引き続き
        if (players.Length <= 1)
        {
            Debug.Log("Awake");
            DontDestroyOnLoad(this.gameObject);
        }
        // 重複している分を削除
        // プレイヤー
        else
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                //Debug.Log((i + 1) + "人目削除");
                Destroy(players[i]);
            }

            Debug.Log("重複しているプレイヤーを削除");
        }
        // メインカメラ
        if (cameras.Length > 1)
        {
            for(int i = 1; i < cameras.Length; i++)
            {
                //Debug.Log((i+1) + "台目削除");
                Destroy(cameras[i]);
            }

            Debug.Log("重複しているメインカメラを削除");
        }

        room = GameObject.FindGameObjectsWithTag("Room");
        if (firstRoom != null)
        {
            Debug.Log("firstRoom = " + firstRoom.name);

            for (int i = 0; i < room.Length; i++)
            {
                //Debug.Log((i+1) + "回目");

                // 最初の部屋なら表示する
                if (room[i].name == firstRoom.name)
                {
                    Debug.Log("firstRoom表示");
                    room[i].SetActive(true);
                }
                // その他は非表示
                else
                {
                    Debug.Log("その他非表示");
                    room[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("firstRoomない");
        }
    }

    ///////////////////////////////////////
    /// 初期化
    //////////////////////////////////////

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        if (players.Length <= 1)
        {
            Debug.Log("Start");

            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            rigid.centerOfMass = centor;
            layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        }
    }

    ///////////////////////////////////////
    /// 更新
    //////////////////////////////////////

    void Update()
    {

        //　キャラクターが接地している場合
        if (isGrounded)
        {
            //　接地したので移動速度を0にする
            velocity = Vector3.zero;
            input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //　方向キーが多少押されている
            if (input.magnitude > 0f)
            {
                animator.SetFloat("Speed", input.magnitude);

                // 向きを変更
                //transform.LookAt(transform.position + input); 

                Quaternion targetRotation = Quaternion.LookRotation(input);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

                velocity += transform.forward * walkSpeed;

            //　キーの押しが小さすぎる場合は移動しない
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

            //　ジャンプ
            if (Input.GetButtonDown("Jump")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                && !animator.IsInTransition(0)      //　遷移途中にジャンプさせない条件
            )
            {
                animator.SetBool("Jump", true);

                //　ジャンプしたら接地していない状態にする
                isGrounded = false;
                velocity.y += jumpPower;
            }
        }
    }
    void FixedUpdate()
    {
        //　キャラクターを移動させる処理
        rigid.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //　地面に着地したかどうかの判定
        if (Physics.CheckSphere(transform.position, 0.3f, layerMask))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
            velocity.y = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //　地面から降りた時の処理
        //　Fieldレイヤーのゲームオブジェクトから離れた時
        if (1 << collision.gameObject.layer != layerMask)
        {
            //　下向きにレイヤーを飛ばしFieldレイヤーと接触しなければ地面から離れたと判定する
            if (!Physics.Linecast(transform.position + Vector3.up * 0.2f, transform.position + Vector3.down * 0.1f, LayerMask.GetMask("Field", "Block")))
            {
                isGrounded = false;
            }
        }
    }

    // 空間移動
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("gate当たった");

        // テレポート
        if (other.gameObject.tag == "Gate")
        {
            switch (eatialMovementPattern)
            {
                case EpatialMovementPattern.Teleportation:   Rum_Teleportation(other); break;
                case EpatialMovementPattern.SceneMovement:   Rum_SceneMovement(other); break;
                case EpatialMovementPattern.ObjectSwitching: Rum_ObjectSwitching(other); break;
            }
        }
    }

    // 瞬間移動
    void Rum_Teleportation(Collider other)
    {
        if (other.gameObject == gateA)
        {
            Debug.Log("gateA");

            transform.position = gateB.transform.position + new Vector3(0, 2, 0);
        }

        if (other.gameObject == gateB)
        {
            Debug.Log("gateB");
            transform.position = gateA.transform.position + new Vector3(0, 2, 0);
        }
    }

    // シーン移動
    void Rum_SceneMovement(Collider other)
    {
        // 現在のシーン名を取得
        string sceneName = SceneManager.GetActiveScene().name;

        // A → B
        if (sceneName == sceneA.m_SceneName)
        {
            Debug.Log(sceneName + "→" + sceneB.m_SceneName);

            // 移動先
            SceneManager.LoadScene(sceneB);
        }

        // B → A
        else if (sceneName == sceneB.m_SceneName)
        {
            Debug.Log(sceneName + "→" + sceneA.m_SceneName);

            SceneManager.LoadScene(sceneA);
        }
    }

    // オブジェクト切り替え
    void Rum_ObjectSwitching(Collider other)
    {
        if(room == null) { return; }

        if(room.Length <= 1)
        {
            Debug.Log("切り替え先がない！");
            return;
        }

        // 表示中のオブジェクトを検索
        ActiveRoom = GameObject.FindGameObjectsWithTag("Room");

        for (int i = 0; i < ActiveRoom.Length; i++)
        {
            // Room1 → 2
            if (ActiveRoom[i] == room1)
            {
                Debug.Log("Room1 → 2");

                room1.SetActive(false);
                room2.SetActive(true);
            }

            // Room2 → 1
            else if (ActiveRoom[i] == room2)
            {
                Debug.Log("Room2 → 1");

                room1.SetActive(true);
                room2.SetActive(false);
            }
        }
    }
}