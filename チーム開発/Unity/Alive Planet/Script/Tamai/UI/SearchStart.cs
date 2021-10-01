using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchStart : MonoBehaviour
{
    [SerializeField] private MultiDatas multiDatas;

    //[SerializeField] private GameObject droneData_UIInstance; // DroneDataのUIインスタンス

    public void Button_SearchStart()
    {
        //droneData_UIInstance.SetActive(false);
        this.gameObject.SetActive(false);

        // 探索開始
        multiDatas.searchStatus = SearchStatus.search;
        multiDatas.GetDroneManager.Start();
    }
}