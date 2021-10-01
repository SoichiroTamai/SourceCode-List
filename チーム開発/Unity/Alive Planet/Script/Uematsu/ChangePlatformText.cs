using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlatformText : MonoBehaviour
{
    //変更するテキスト
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform==RuntimePlatform.WindowsEditor)
        {
            text.text = "左クリックで次へ";
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            text.text = "画面タップで次へ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
