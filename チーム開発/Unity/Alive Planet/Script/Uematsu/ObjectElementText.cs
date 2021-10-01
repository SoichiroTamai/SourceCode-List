using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectElementText : MonoBehaviour
{
    //クリックしたオブジェクト
    public GetClickObject clickObject;

    //船の情報のスクリプト
    private ShipStatus shipScript;

    //船の情報を返す変数
    private ShipStatus hideShipStatus;

    //惑星の情報のスクリプト
    private PlanetStatus planetScript;

    //テキストを出したり消したりするためのオブジェクト
    public GameObject ShipTextObject;
    public GameObject PlanetTextObject;

    //文字を出すテキスト
    public Text ResourceText;
    public Text FuelText;
    public Text ClearText;

    private void Update()
    {
        //オブジェクトが選択されてなければ返す
        if(clickObject.GetClickedObject()==null)
        {
            return;
        }

        //選択されたオブジェクトに対応するステータスを表示する
        if(clickObject.GetClickedObject().tag=="Ship")
        {
            TextShip(clickObject.GetClickedObject());
        }
        else if (clickObject.GetClickedObject().tag == "Planet")
        {
            TextPlanet(clickObject.GetClickedObject());
        }
    }

    public void TextShip(GameObject shipObject)
    {
        //船の情報を出して惑星の情報を消す
        ShipTextObject.SetActive(true);
        PlanetTextObject.SetActive(false);

        //クリックしたオブジェクトを参照し変数を引き出す
        shipScript = shipObject.GetComponent<ShipStatus>();

        hideShipStatus = shipScript;

        //引き出した引数をテキストで表示する
        ResourceText.text = string.Format("Resource:{000}", shipScript.ResourceNum);
        FuelText.text = string.Format("Fuel:{000}", shipScript.FuelNum);
    }

    public void TextPlanet(GameObject planetObject)
    {
        //惑星の情報を出して船の情報を消す
        ShipTextObject.SetActive(false);
        PlanetTextObject.SetActive(true);

        //上と同じ
        planetScript = planetObject.GetComponent<PlanetStatus>();
        ClearText.text = string.Format("Safe:{000}%", planetScript.Probability);
    }

    public bool GetClear() 
    {
        GameObject planetObject = clickObject.GetClickedObject();
        planetScript = planetObject.GetComponent<PlanetStatus>();
        if (clickObject.GetClickedObject().tag == "Planet")
        {
            TextPlanet(clickObject.GetClickedObject());
            return planetScript.GetIsClear();
        }
        return false;
    }

    public ShipStatus GetShipParam()
    {
        if (!hideShipStatus) { return null; }
        return hideShipStatus;
    }
}
