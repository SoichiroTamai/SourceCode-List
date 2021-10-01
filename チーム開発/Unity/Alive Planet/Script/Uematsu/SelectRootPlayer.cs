using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRootPlayer : MonoBehaviour
{
    public GetClickObject getClickObject;

    //資材の量とかを管理しているスクリプト
    public GameMasterScript masterScript;

    //距離を表示するテクスチャ
    public Text text;
    //ゲームマネージャー
    public GameMasterScript GameMaster;
    //飛行機の移動するスピード
    public float speed = 0.05f;
    //飛行機の移動する前の位置
    private Vector3 StartPos;
    //現在の燃料を表示するテキスト
    public Text FuelVaultText;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GameMaster.PlayerPos;
        FuelVaultText.text ="燃料：" + masterScript.FuelVault.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(getClickObject.GetClickedObject()==null)
        {
            return;
        }

        text.text=string.Format("{000}m", SelectObjectDistance()) ;

        if(CanMove())
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }

    //クリックしているオブジェクトとプレイヤーの距離を返す関数
    public int SelectObjectDistance()
    {
        Vector3 PlayerPos = this.transform.position;
        GameObject ClickObject = getClickObject.GetClickedObject();
        Vector3 ClickObjectPos = ClickObject.transform.position;
        float distance = Vector3.Distance(PlayerPos, ClickObjectPos);

        return (int)distance;
    }

    public void SetStartPos()
    {
        StartPos = this.transform.position;
    }

    public bool CanMove()
    {
        return SelectObjectDistance() > masterScript.FuelVault;
    }
    
    //この関数をUpdateで呼べば動く
    public bool MovePlayer()
    {
        //プレイヤーの座標をクリックしているオブジェクトに合わせる
        //this.transform.position = getClickObject.GetClickedObject().transform.position;
        //合わせた座標をゲームマネージャーに渡す
        //GameMaster.PlayerPos = this.transform.position;

        //現在の位置
        float present_Location = (Time.time * speed) / SelectObjectDistance();
        //オブジェクトの移動
        this.transform.position = Vector3.Lerp(StartPos, getClickObject.GetClickedObject().transform.position, present_Location);
        
        //オブジェクトがゴールまで着いたら
        if(present_Location>=1)
        {
            //その場所を格納し処理を終了
            GameMaster.PlayerPos = this.transform.position;
            return true;
        }

        return false;
    }
}
