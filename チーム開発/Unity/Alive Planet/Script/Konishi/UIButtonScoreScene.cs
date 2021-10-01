using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonScoreScene : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Repair");
    }
}
