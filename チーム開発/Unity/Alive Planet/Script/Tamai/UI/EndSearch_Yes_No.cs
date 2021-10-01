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

        // �擾�����������Q�[���}�X�^�[��
        gameMaster.ResouceVault = ResourceScript.i_resourceRecoveredSum;

        // �X�R�A�\���V�[���Ɉړ�
        SceneManager.LoadScene("ScoreScene");
    }

    public void Button_No()
    {
        quitEnd_UIInstance = GameObject.Find("Canvas_QuitEnd(Clone)");
        quitEnd_UIInstance.SetActive(false);
    }
}