using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUIReturn : MonoBehaviour
{
    public EventPadOpen padOpen;

    //�{�^�����N���b�N���ꂽ��UI������
    public void OnClick()
    {
        padOpen.SetPadClosePos();
    }
}
