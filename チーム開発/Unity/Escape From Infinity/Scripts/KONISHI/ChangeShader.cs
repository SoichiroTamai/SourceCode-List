using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    public ObjectScript objScript;

    // シェーダーを変更したか
    private bool changeShaderFlg;

    // Start is called before the first frame update
    void Start()
    {
        changeShaderFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // レイが当たるか掴んでたら
        if (objScript.RayCheck() || objScript.GetIsCatch())
        {
            if (!changeShaderFlg)
            {
                // 全てのマテリアルのシェーダーをLimOutLineに変更
                for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
                {
                    gameObject.GetComponent<Renderer>().materials[i].shader = Shader.Find("Custom/LimOutline");
                    gameObject.GetComponent<Renderer>().materials[i].SetColor("_OutlineColor", Color.green);
                }
                changeShaderFlg = true;
            }
        }
        else
        {
            // 全てのマテリアルのシェーダーをStandardに変更
            for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                gameObject.GetComponent<Renderer>().materials[i].shader = Shader.Find("Standard");
            }
            changeShaderFlg = false;
        }
    }
}
