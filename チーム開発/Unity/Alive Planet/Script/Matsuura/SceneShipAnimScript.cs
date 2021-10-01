using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShipAnimScript : MonoBehaviour
{
    public GameObject ShipTexture1;

    public GameObject ShipTexture2;

    public GameObject ShipTexture3;

    public GameObject ShipTexture4;

    public GameObject ShipTexture5;

    public GameObject ShipTexture6;

    public GameObject BlackTexture;

    public GameObject mainCamera;

    private bool b_create;

    // Start is called before the first frame update
    void Start()
    {
        if (!mainCamera) { return; }
        b_create = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShipTexture1 || !ShipTexture2 || !ShipTexture3 || !ShipTexture4 || !ShipTexture5 || !ShipTexture6) { return; }

        int RandomTexture;

        {
            RandomTexture = Random.Range(0, 6);
        }

        switch (RandomTexture)
        {
            case 0:
                if (!b_create)
                {
                    Instantiate(ShipTexture1, new Vector2(mainCamera.transform.position.x + 10.0f, mainCamera.transform.position.y), Quaternion.identity);
                    ShipTexture1.GetComponent<ShipAnimScript>();
                    BlackFrontScript BFScript = BlackTexture.GetComponent<BlackFrontScript>();
                    BFScript.Flg = true;
                }
                b_create = true;
                break;
            case 1:
                if (!b_create)
                {
                    Instantiate(ShipTexture2, new Vector2(mainCamera.transform.position.x + 10.0f, mainCamera.transform.position.y), Quaternion.identity);
                    ShipTexture2.GetComponent<ShipAnimScript>();
                    BlackFrontScript BFScript = BlackTexture.GetComponent<BlackFrontScript>();
                    BFScript.Flg = true;
                }
                b_create = true;
                break;
            case 2:
                if (!b_create)
                {
                    Instantiate(ShipTexture3, new Vector2(mainCamera.transform.position.x + 10.0f, mainCamera.transform.position.y), Quaternion.identity);
                    ShipTexture3.GetComponent<ShipAnimScript>();
                    BlackFrontScript BFScript = BlackTexture.GetComponent<BlackFrontScript>();
                    BFScript.Flg = true;
                }
                b_create = true;
                break;
            case 3:
                if (!b_create)
                {
                    Instantiate(ShipTexture4, new Vector2(mainCamera.transform.position.x + 10.0f, mainCamera.transform.position.y), Quaternion.identity);
                    ShipTexture4.GetComponent<ShipAnimScript>();
                    BlackFrontScript BFScript = BlackTexture.GetComponent<BlackFrontScript>();
                    BFScript.Flg = true;
                }
                b_create = true;
                break;
            case 4:
                if (!b_create)
                {
                    Instantiate(ShipTexture5, new Vector2(mainCamera.transform.position.x + 10.0f, mainCamera.transform.position.y), Quaternion.identity);
                    ShipTexture5.GetComponent<ShipAnimScript>();
                    BlackFrontScript BFScript = BlackTexture.GetComponent<BlackFrontScript>();
                    BFScript.Flg = true;
                }
                b_create = true;
                break;
            case 5:
                if (!b_create)
                {
                    Instantiate(ShipTexture6, new Vector2(mainCamera.transform.position.x + 10.0f, mainCamera.transform.position.y), Quaternion.identity);
                    ShipTexture6.GetComponent<ShipAnimScript>();
                    BlackFrontScript BFScript = BlackTexture.GetComponent<BlackFrontScript>();
                    BFScript.Flg = true;
                }
                b_create = true;
                break;
        }
    }

    public void AnimationOn()
    {
        b_create = false;
    }
}
