using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DroneRepair : MonoBehaviour
{
    [SerializeField] Drone.DroneData droneData;
    [SerializeField] int fuelMax;   // 最大燃料
    [SerializeField] int fuel;      // 燃料
    [SerializeField] int resMax;    // 最大資源

    public MotherShip motherShip;

    //リソースとかを管理するゲームオブジェクト
    public GameMasterScript masterScript;

    public Slider resSlider;

    //修理出来ない時に表示するUI
    public Image image;
    private bool imageFlg = false;

    private float FadeSpeed = 0.003f;//透明度が変わるスピードを管理
    float alpha = 1;                      //パネルの不透明度を管理

    public Text ResourceText;


    // 修理
    public void Repair()
    {
        if (fuel < fuelMax || droneData.droneRes < resMax)
        {
            // 消費量
            int resConsume = (int)resSlider.value;

            // 修理に必要な量があったら
            if (masterScript.ResouceVault>= resConsume)
            {

                if (resMax - droneData.droneRes < resConsume)
                {
                    resConsume = resMax - droneData.droneRes;
                }

                // 資源補充
                Debug.Log("生成前"+masterScript.ResouceVault);
                masterScript.ResouceVault = masterScript.ResouceVault - resConsume;
                droneData.droneRes += resConsume;
                Debug.Log("生成後" + masterScript.ResouceVault);
            }
            else
            {
                Debug.Log("生成出来ません");
                // 出撃不可能
                image.gameObject.SetActive(true);
                imageFlg = true;
                alpha = 1;
            }
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

    //セッターとゲッター
    public int GetFuel() { return fuel; }

    public int GetRes() { return droneData.droneRes; }

    public void SetFuel(int Num) { fuel = Num; }

    public void SetRes(int Num) { droneData.droneRes = Num; }

    // Start is called before the first frame update
    void Start()
    {
        fuelMax = 10;
        resMax = 5;
        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            fuel = 0;
            droneData.droneRes = 0;
        }

        if (imageFlg)
        {
            FadeOut();
        }

        ResourceText.text = "資材：" + masterScript.ResouceVault;

        if(masterScript.ResouceVault >= (int)resSlider.value)
        {
            ResourceText.color = Color.white;
        }
        else
        {
            ResourceText.color = Color.red;
        }
    }
}
