using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUIReturn : MonoBehaviour
{
    public EventPadOpen padOpen;

    //ボタンがクリックされたらUIを消す
    public void OnClick()
    {
        padOpen.SetPadClosePos();
    }
}
