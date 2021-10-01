using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutorSafe : MonoBehaviour
{
    //クリックしたオブジェクト
    public GetClickObject clickObject;

    //惑星の情報のスクリプト
    private PlanetStatus planetScript;
    private bool click;

    private GameObject clickobject;

    int landing;

    public Text text;


    private void Start()
    {
        text = gameObject.AddComponent<Text>();

        clickobject = GameObject.Find("GetClickObject");
    }

    private void Update()
    {
        clickObject = clickobject.GetComponent<GetClickObject>();

        //オブジェクトが選択されてなければ返す
        if (!clickObject.GetClickedObject())
        {
            text.text = "記録無し";
            return;
        }

        //選択されたオブジェクトに対応するステータスを表示する
        if (clickObject.GetClickedObject().tag == "Planet")
        {
            Planet(clickObject.GetClickedObject());
        }
    }

    public void Planet(GameObject planetObject)
    {
        Debug.Log("記録なし");

        //上と同じ
        planetScript = planetObject.GetComponent<PlanetStatus>();
        int landing = planetScript.Probability;

        //そのパーセントで成功するかどうかを出す
        int planet = planetScript.probability;
        Debug.Log(planet);
        //さっき出した数より前に出した数の方が大きければ成功
        if (planet < landing)
        {
            text.text = "成功!!!!!!!!!!!!!!!!!!!!!!!";
        }
        else
        {
            text.text = "失敗!!!!!!!!!!!!!!!!!!!!!!!!";
        }
    }
}
