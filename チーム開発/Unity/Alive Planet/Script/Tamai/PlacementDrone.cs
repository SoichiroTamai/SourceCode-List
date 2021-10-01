using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドローン配置画面
public class PlacementDrone : MonoBehaviour
{
    [SerializeField] private MultiDatas multiDatas;  // 探索シーンでの共有データ

    public Drone.DroneType createDroneType;

    // Start is called before the first frame update
    void Start()
    {
        Placement_Start();
    }

    private void Placement_Start()
    {
        if (multiDatas.searchStatus != SearchStatus.placement) { return; }

        // 追従するオブジェクトを指定
        //multiDatas.cameraFollowObject = multiDatas.mainCamera;                      // 追従するオブジェクトの設定
        //multiDatas.virtualCamera.Follow = multiDatas.cameraFollowObject.transform;  // 情報を反映
    }

    // Update is called once per frame
    void Update()
    {
        if (multiDatas.searchStatus != SearchStatus.placement) { return; }

        Placement();
    }

    void Placement()
    {
        GameObject clickObject = multiDatas.clickObject;
        if (clickObject == null) { return; }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            switch (clickObject.tag)
            {
                case "MapTile":
                    //case "Resource":
                    Placement_Drone();
                    break;

                case "Drone":
                    ClickDrone();
                    break;
            }
        }
    }

    void Placement_Drone()
    {
        if(createDroneType == Drone.DroneType.none) { return; }

        // 生成
        multiDatas.GetDroneManager.InstantiateDrone(createDroneType, multiDatas.clickObject.transform.position);

        // 配置したオブジェクトの位置情報を格納
        multiDatas.droneSelect.placementData[multiDatas.droneSelect.dropDownDatas.value].pos = multiDatas.clickObject.transform.position;

        // 配置したオブジェクトを選択不可に
        multiDatas.droneSelect.placementData[multiDatas.droneSelect.dropDownDatas.value].toggles_Interactable = false;

        // 未選択に初期化
        createDroneType = Drone.DroneType.none;
        multiDatas.droneSelect.dropDownDatas.value = 0;

        multiDatas.clickObject = null;
    }

    void ClickDrone()
    {
        // リストから選択したドローンと同じ場所のドローンを探し選択可能に戻す
        foreach(var pd in multiDatas.droneSelect.placementData)
        {
            if (pd.pos == multiDatas.clickObject.transform.position)
            {
                // 選択可能に
                pd.toggles_Interactable = true;
                break;
            }
        }

        Destroy(multiDatas.clickObject);
    }
}