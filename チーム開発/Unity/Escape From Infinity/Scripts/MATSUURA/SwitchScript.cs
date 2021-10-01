using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwitchNumber;

public class SwitchScript : MonoBehaviour
{
    private SwitchObjectNumber ObjectNumber;

    public SwitchObjectNumber GetObjNumber
    {
        get { return ObjectNumber; }
    }

    private Rigidbody rd;

    private SwitchSystemScript SSS;

    public GameObject MotherSystem;

    public int SwitchNumberHolder;

    public bool Default;

    private Vector3 v_defPotision;

    // Start is called before the first frame update
    void Start()
    {
        Default = false;

        SSS = MotherSystem.GetComponent<SwitchSystemScript>();

        v_defPotision = transform.position;

        rd = GetComponent<Rigidbody>();

        switch (SwitchNumberHolder)
        {
            case 0:
                ObjectNumber = SwitchObjectNumber.NONE;
                break;
            case 1:
                ObjectNumber = SwitchObjectNumber.e_ONE;
                break;
            case 2:
                ObjectNumber = SwitchObjectNumber.e_TWO;
                break;
            case 3:
                ObjectNumber = SwitchObjectNumber.e_THREE;
                break;
            case 4:
                ObjectNumber = SwitchObjectNumber.e_FOUR;
                break;
            case 5:
                ObjectNumber = SwitchObjectNumber.e_FIVE;
                break;
            case 6:
                ObjectNumber = SwitchObjectNumber.e_SIX;
                break;
            case 7:
                ObjectNumber = SwitchObjectNumber.e_SEVEN;
                break;
            case 8:
                ObjectNumber = SwitchObjectNumber.e_EIGHT;
                break;
            case 9:
                ObjectNumber = SwitchObjectNumber.e_NINE;
                break;
            case 10:
                ObjectNumber = SwitchObjectNumber.e_TEN;
                break;
            default:
                ObjectNumber = SwitchObjectNumber.NONE;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Default)
        {
            rd.AddForce(v_defPotision);
            Default = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Vector3 vec = transform.position;

            vec.y -= 1;

            transform.position = vec;

            SSS.AddSwitchList(SwitchNumberHolder);
        }
    }
}
