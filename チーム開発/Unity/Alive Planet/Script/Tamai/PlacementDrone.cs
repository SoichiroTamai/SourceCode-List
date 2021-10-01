using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �h���[���z�u���
public class PlacementDrone : MonoBehaviour
{
    [SerializeField] private MultiDatas multiDatas;  // �T���V�[���ł̋��L�f�[�^

    public Drone.DroneType createDroneType;

    // Start is called before the first frame update
    void Start()
    {
        Placement_Start();
    }

    private void Placement_Start()
    {
        if (multiDatas.searchStatus != SearchStatus.placement) { return; }

        // �Ǐ]����I�u�W�F�N�g���w��
        //multiDatas.cameraFollowObject = multiDatas.mainCamera;                      // �Ǐ]����I�u�W�F�N�g�̐ݒ�
        //multiDatas.virtualCamera.Follow = multiDatas.cameraFollowObject.transform;  // ���𔽉f
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

        // ����
        multiDatas.GetDroneManager.InstantiateDrone(createDroneType, multiDatas.clickObject.transform.position);

        // �z�u�����I�u�W�F�N�g�̈ʒu�����i�[
        multiDatas.droneSelect.placementData[multiDatas.droneSelect.dropDownDatas.value].pos = multiDatas.clickObject.transform.position;

        // �z�u�����I�u�W�F�N�g��I��s��
        multiDatas.droneSelect.placementData[multiDatas.droneSelect.dropDownDatas.value].toggles_Interactable = false;

        // ���I���ɏ�����
        createDroneType = Drone.DroneType.none;
        multiDatas.droneSelect.dropDownDatas.value = 0;

        multiDatas.clickObject = null;
    }

    void ClickDrone()
    {
        // ���X�g����I�������h���[���Ɠ����ꏊ�̃h���[����T���I���\�ɖ߂�
        foreach(var pd in multiDatas.droneSelect.placementData)
        {
            if (pd.pos == multiDatas.clickObject.transform.position)
            {
                // �I���\��
                pd.toggles_Interactable = true;
                break;
            }
        }

        Destroy(multiDatas.clickObject);
    }
}