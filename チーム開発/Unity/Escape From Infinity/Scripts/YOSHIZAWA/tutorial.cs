using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    //チュートリアルパネルを追加する
    public GameObject TutorialCanvas;

    public GameObject PointCanvas;

    public GameObject MebiusCanvas;

    // 外部クラスで使用
    [HideInInspector]
    public static bool isTutorialCanvas; // TutorialCanvas が表示中か (true = 表示中)

    // Start is called before the first frame update
    void Start()
    {
        PointCanvas.SetActive(false);
        MebiusCanvas.SetActive(false);

        isTutorialCanvas = TutorialCanvas.activeSelf;

        if (TutorialCanvas.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 表示している時は TAB で閉じる
        if (Input.GetKeyDown(KeyCode.Tab) && TutorialCanvas.activeSelf)
        {
            TutorialCanvas_SetActive(false);
            PauseScript.changeTutorialUI = false;

            isTutorialCanvas = false;
            
            if (PauseScript.isPouse == false)
            {
                Time.timeScale = 1.0f;
            }
        }

        if (PauseScript.changeTutorialUI != isTutorialCanvas)
        {
            if (TutorialCanvas.activeSelf) { return; }
            
            TutorialCanvas_SetActive(true);
            isTutorialCanvas = true;
        }
    }

    //チュートリアルのキャンバスを表示切替
    public void TutorialCanvas_SetActive(bool isActive)
    {
        TutorialCanvas.SetActive(isActive);
        PointCanvas.SetActive(!isActive);
        MebiusCanvas.SetActive(!isActive);

        //TutorialCanvas.SetActive(!TutorialCanvas.activeSelf);
        //PointCanvas.SetActive(!PointCanvas.activeSelf);
        //MebiusCanvas.SetActive(!MebiusCanvas.activeSelf);
        //MebiusCanvas.SetActive(!MebiusCanvas.activeSelf);
    }
}
