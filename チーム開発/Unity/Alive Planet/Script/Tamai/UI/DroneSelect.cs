using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Drone;
using UnityEngine.EventSystems;

public class DroneSelect : MonoBehaviour
{
    [SerializeField] private MultiDatas     multiDatas;
    [SerializeField] private PlacementDrone placementDrone;

    [HideInInspector] public Dropdown dropDownDatas;

    // タイプ別固体番号 0…攻撃 1…情報 2…運搬
    private int[] typeNum = new int[3];

    private int oldValue;

    // 設置情報
    public class PlacementData
    {
        public bool      toggles_Interactable; // 選択可能か ※false … 設置済み
        public Vector3   pos;                  // 設置場所
        public DroneType type;

        public PlacementData(bool _interactable = true, Vector3 _pos = new Vector3(), DroneType _type = DroneType.none)
        {
            toggles_Interactable = _interactable;
            pos  = _pos;
            type = _type;
        }
    }

    public List<PlacementData> placementData;

    // Start is called before the first frame update
    void Start()
    {
        dropDownDatas = this.gameObject.GetComponent<Dropdown>();

        placementData = new List<PlacementData>();

        if (dropDownDatas)
        {            
            // 現在の要素をクリア
            dropDownDatas.ClearOptions();

            List<string> list = new List<string>();
            list.Add("None");
            placementData.Add(new PlacementData(true));

            if (multiDatas.GetDroneDatas == null)
            {
                dropDownDatas.AddOptions(list);
                return;
            }

            // GameMasterのドローン情報を基にリストを作成
            if (multiDatas.GetDroneDatas.Count > 0)
            {
                foreach (var data in multiDatas.GetDroneDatas)
                {
                    switch (data.droneType)
                    {
                        case DroneType.attack:
                            typeNum[0]++;
                            list.Add(typeNum[0] + ". " + data.name.Substring(5));
                            placementData.Add(new PlacementData(_type: DroneType.attack));
                            break;

                        case DroneType.information:
                            typeNum[1]++;
                            list.Add(typeNum[1] + ". " + data.name.Substring(5));
                            placementData.Add(new PlacementData(_type: DroneType.information));
                            break;

                        case DroneType.transport:
                            typeNum[2]++;
                            list.Add(typeNum[2] + ". " + data.name.Substring(5));
                            placementData.Add(new PlacementData(_type: DroneType.transport));
                            break;
                    }
                }
            }

            // リストに追加
            dropDownDatas.AddOptions(list);

            // 初期値を設定
            dropDownDatas.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 選択可能なドローン情報をバックアップより復元
        var _toggles = GetComponentsInChildren<Toggle>();

        if (_toggles.Length == placementData.Count)
        {
            for (int i = 1; i < placementData.Count; i++)
            {
                _toggles[i].interactable = placementData[i].toggles_Interactable;
            }
        }

        if (dropDownDatas.value <= 0) { return; }

        if (dropDownDatas.value == oldValue) { return; }

        oldValue = dropDownDatas.value;

        //// 初回 (選択)
        //if(placementData.Count <= 0)
        //{
        //    foreach(var ti in _toggles)
        //    {
        //        PlacementData pd = new PlacementData(ti.interactable);
        //        placementData.Add(pd);
        //    }
        //}

        // 選択したドローンのタイプを生成するドローンに指定
        try
        {
            placementDrone.createDroneType = placementData[dropDownDatas.value].type;
        }
        catch
        {
            placementDrone.createDroneType = DroneType.none;
        }
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    toggles = GetComponentsInChildren<Toggle>();

    //    toggles[dropDownDatas.value].interactable = false;
    //}
}