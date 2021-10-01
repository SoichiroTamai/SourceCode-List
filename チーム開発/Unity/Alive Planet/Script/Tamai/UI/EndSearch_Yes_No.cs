using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSearch_Yes_No : MonoBehaviour
{
    private GameObject quitEnd_UIInstance;
    GameMasterScript gameMaster;
    ResouceScript ResourceScript;

    public void Button_Yes()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
        ResourceScript = GameObject.Find("ResouceManager").GetComponent<ResouceScript>();

        // 取得した資源をゲームマスターに
        gameMaster.ResouceVault = ResourceScript.i_resourceRecoveredSum;

        // スコア表示シーンに移動
        SceneManager.LoadScene("ScoreScene");
    }

    public void Button_No()
    {
        quitEnd_UIInstance = GameObject.Find("Canvas_QuitEnd(Clone)");
        quitEnd_UIInstance.SetActive(false);
    }
}