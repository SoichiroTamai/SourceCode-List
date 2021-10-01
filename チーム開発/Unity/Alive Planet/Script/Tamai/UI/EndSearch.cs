using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSearch : MonoBehaviour
{
    [SerializeField]
    private GameObject quitEnd_UI;         // 終了して良いか確認する為のUIオブジェクト
    private GameObject quitEnd_UIInstance; // EndUIのインスタンス

    private void Start()
    {
        // インスタンス化, 非表示
        quitEnd_UIInstance = GameObject.Instantiate(quitEnd_UI) as GameObject;
        quitEnd_UIInstance.SetActive(false);
    }

    public void Button_End_of_search()
    {
        quitEnd_UIInstance.SetActive(true);
    }
}