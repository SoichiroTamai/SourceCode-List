using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetExplorationScene : MonoBehaviour
{
    public void ButtonClick()
    {
        SceneManager.LoadScene("ResourceManagementScene");
    }
}
