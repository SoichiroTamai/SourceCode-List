using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourasRotation : MonoBehaviour
{
    enum Rotations
    {
        plusX,
        minusX,
        plusY,
        minusY,
    }

    private Rotations rotation;

    public int RotationNumber;

    // Start is called before the first frame update
    void Start()
    {
        switch (RotationNumber)
        {
            case 0:
                rotation = Rotations.plusX;
                break;
            case 1:
                rotation = Rotations.minusX;
                break;
            case 2:
                rotation = Rotations.plusY;
                break;
            case 3:
                rotation = Rotations.minusY;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (rotation)
        {
            case Rotations.plusX:
                transform.Rotate(new Vector3(0.02f, 0, 0));
                break;
            case Rotations.minusX:
                transform.Rotate(new Vector3(-0.02f, 0, 0));
                break;
            case Rotations.plusY:
                transform.Rotate(new Vector3(0, 0.02f, 0));
                break;
            case Rotations.minusY:
                transform.Rotate(new Vector3(0, -0.02f, 0));
                break;
            default:
                transform.Rotate(new Vector3(0, 0.01f, 0));
                break;
        }

    }
}
