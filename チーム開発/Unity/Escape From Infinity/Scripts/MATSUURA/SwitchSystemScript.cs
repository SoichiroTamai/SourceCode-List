using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwitchNumber
{
    public enum SwitchObjectNumber
    {
        NONE,
        e_ONE,
        e_TWO,
        e_THREE,
        e_FOUR,
        e_FIVE,
        e_SIX,
        e_SEVEN,
        e_EIGHT,
        e_NINE,
        e_TEN
    }
}

public class SwitchSystemScript : MonoBehaviour
{
    private List<int> l_allSwitch = new List<int>();

    private List<int> l_activeSwitch = new List<int>();

    public int LinkSwitch;

    public GameObject GimmicObject;

    private bool b_CompleateGimmic;

    private bool b_notCompleateGimmic;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < LinkSwitch + 1; i++)
        {
            l_allSwitch.Add(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (l_activeSwitch.Count == LinkSwitch)
        {
            for (int i = 0; i < LinkSwitch; i++)
            {
                if (l_allSwitch[i] != l_activeSwitch[i])
                {
                    b_notCompleateGimmic = true;
                    break;
                }
            }
            b_CompleateGimmic = true;
        }


        if (!b_notCompleateGimmic && b_CompleateGimmic)
        {
            Vector3 vec = GimmicObject.gameObject.transform.position;

            vec.y += 0.05f;

            GimmicObject.gameObject.transform.position = vec;
        }
    }

    public void AddSwitchList(int SwitchNumber)
    {
        l_activeSwitch.Add(SwitchNumber);
    }

}
