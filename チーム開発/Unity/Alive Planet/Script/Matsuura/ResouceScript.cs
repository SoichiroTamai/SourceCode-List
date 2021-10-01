using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResouceScript : MonoBehaviour
{
    private GameObject go_BlockPosition;

    private ObjectElementText objectElementText;

    private int i_allResouce = 0;

    private bool b_compleate;

    public GameObject ResouceObject;

    [HideInInspector]
    public int i_resourceRecoveredSum; // 探索時に回収した資源の数

    public AudioClip audioClip;
    AudioSource audioSource;

    // UI
    [SerializeField]
    private Text resourceRecovered_Sum;
    private string defaultText;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!ResouceObject)
        {
            Debug.Log("ResouceScript (ResouceObject)参照出来ません");
            return;
        }

        go_BlockPosition = GameObject.Find("MapCreateManager");

        // 配置する資源数
        i_allResouce = Random.Range(1, 6);

        //i_allResouce = objectElementText.GetShipParam().ResourceNum;

        //if (i_allResouce == 0)
        //{

        //    {
        //        i_allResouce = Random.Range(0, 6);
        //    }
        //}

        b_compleate = false;
    
        // 回収した資源数
        i_resourceRecoveredSum = 0;

        // UI
        defaultText = resourceRecovered_Sum.text;
        UpdateText(0);
    }

    private void Update()
    {
        if (!go_BlockPosition)
        {
            Debug.Log("ResouceScript (go_BlockPotision)参照出来ません");
            return;
        }

        MapCreate mapCreate;

        mapCreate = go_BlockPosition.GetComponent<MapCreate>();

        if (!mapCreate)
        {
            Debug.Log("ResouceScript (mapCreate)参照出来ません");
            return;
        }

        if (mapCreate.MapCreateCompleate())
        {
            if (!b_compleate)
            {
                for (int i = 0; i < i_allResouce; i++)
                {
                    int Rand = 2;

                    {
                        Rand = Random.Range(2, mapCreate.MapBlockList.Count);
                    }

                    Vector2 vec;

                    vec = mapCreate.MapBlockList[Rand].transform.position;

                    Instantiate(ResouceObject, vec, Quaternion.identity);
                    Debug.Log("資源配置完了");
                }
                b_compleate = true;
            }

        }
    }

    // 資源回収 (資源確保時に呼び出される)
    public void AddResourceRecoveredSum()
    {
        audioSource.PlayOneShot(audioClip);
        Debug.Log("資源確保完了");

        i_resourceRecoveredSum++;
        UpdateText(i_resourceRecoveredSum);
    }

    // テキストを更新
    void UpdateText(int resourceRecovered_Num)
    {
        resourceRecovered_Sum.text = defaultText + resourceRecovered_Num;
    }
}