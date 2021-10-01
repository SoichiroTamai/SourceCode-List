using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class ChangeSceneButton : MonoBehaviour
{
    public GameObject ClickObject;

    public GameObject ShipAnimObj;

    public GameObject BlackMonitor;

    private SceneShipAnimScript SSAP;

    private BlackFrontScript BFS;

    private GetClickObject clickObject;

    //プレイヤーのオブジェクト
    public SelectRootPlayer player;
    //レンダーパイプラインのアセット
    public RenderPipelineAsset pipelineAsset;
    //プレイヤーは移動を開始するかどうか
    private bool MoveFlg = false;

    public Image image;
    private bool imageFlg = false;

    public GameMasterScript masterScript;

    private float FadeSpeed = 0.003f;//透明度が変わるスピードを管理
    float alpha = 1;                      //パネルの不透明度を管理

    private void Start()
    {
        if (!ShipAnimObj) { return; }
        if (!BlackMonitor) { return; }
        if (!ClickObject) { return; }

        SSAP = ShipAnimObj.GetComponent<SceneShipAnimScript>();
        BFS = BlackMonitor.GetComponent<BlackFrontScript>();
        clickObject = ClickObject.GetComponent<GetClickObject>();

        //alpha = image.color.a;
        image.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (BFS.GetCompFlg())
        //{
        //    SceneManager.LoadScene("MapCreateTest");
        //}

        if(MoveFlg)
        {
            //プレイヤーを移動させる
            if(player.MovePlayer())
            {
                //ゴール地点に到達していればレンダーパイプラインを設定し次のシーンに移る
                MoveFlg = false;
                SceneManager.LoadScene("MapCreateTest");
            }
        }

        if(imageFlg)
        {
            FadeOut();
        }
    }

    public void OnClick()
    {
        if (!player.CanMove())
        {
            if (clickObject.GetClickedObject().tag == "Ship")
            {
                //SSAP.AnimationOn();
                MoveFlg = true;
                masterScript.FuelVault -= player.SelectObjectDistance();

            }
            else if (clickObject.GetClickedObject().tag == "Planet")
            {
                SceneManager.LoadScene("EDScene");
            }
        }
        else
        {
            image.gameObject.SetActive(true);
            imageFlg = true;
            alpha = 1;
        }
    }

    void FadeOut()
    {
        alpha -= FadeSpeed;//不透明度を徐々に上げる
        image.color = new Color(image.color.r, image.color.g,
            image.color.b, alpha);//変更した不透明度をパネルに反映する

        //0以下になったら処理終了
        if (alpha <= 0)
        {
            imageFlg = false;
            image.gameObject.SetActive(false);
        }
    }
}
