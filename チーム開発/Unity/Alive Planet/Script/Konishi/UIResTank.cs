using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResTank : MonoBehaviour
{
    public MotherShip motherShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UI�ł̃e�L�X�g�\��(�c����)
        Text resText = GetComponent<Text>();
        resText.text = "RESOURCE:" + motherShip.GetResource().ToString();
    }
}
