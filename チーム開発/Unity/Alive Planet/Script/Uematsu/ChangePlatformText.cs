using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlatformText : MonoBehaviour
{
    //�ύX����e�L�X�g
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform==RuntimePlatform.WindowsEditor)
        {
            text.text = "���N���b�N�Ŏ���";
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            text.text = "��ʃ^�b�v�Ŏ���";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
