using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActive : MonoBehaviour
{
    public UIMenu menu;
    public DroneRepair drone;

    public RepairPadOpen repairPadOpen;

    public bool isSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        // ‹­’²•\Ž¦‚ð”ñ•\Ž¦‚É
        menu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GetClickedGameObject>().GetGameObject())
        {
            if (drone.gameObject == GetComponent<GetClickedGameObject>().GetGameObject())
            {
                if (!isSelect)
                {
                    Debug.Log("‘I‘ð‚³‚ê‚½");

                    menu.gameObject.SetActive(true);

                    isSelect = true;
                }
            }
            else
            {
                menu.gameObject.SetActive(false);

                isSelect = false;
            }
        }

        if (repairPadOpen.GetButtonFlg())
        {
            if (isSelect)
            {
                menu.gameObject.SetActive(true);
            }
        }
        else
        {
            menu.gameObject.SetActive(false);
        }
    }
}