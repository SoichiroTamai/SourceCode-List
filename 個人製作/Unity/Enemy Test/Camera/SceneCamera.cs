using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    public Texture2D moveCursor;
    public Texture2D rotCursor;

    [SerializeField, Range(0.0f, 100f)]
    private float wheelSpeed = 50f;

    [SerializeField, Range(0.0f, 100f)]
    private float moveSpeed = 50f;

    [SerializeField, Range(0.0f, 1f)]
    private float rotateSpeed = 0.3f;

    private Vector3 preMousePos;

    private void Update()
    {
        MouseUpdate();
        return;
    }

    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        
        if (scrollWheel != 0.0f)
                MouseWheel(scrollWheel);

        if (Input.GetMouseButtonDown(1) ||
            Input.GetMouseButtonDown(2))
                preMousePos = Input.mousePosition;

        MouseDrag(Input.mousePosition);
    }

    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * wheelSpeed;
        return;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
                return;
        
        if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(rotCursor, Vector2.zero, CursorMode.Auto);
            CameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed);
        }
        else if (Input.GetMouseButton(2))
        {
            Cursor.SetCursor(moveCursor, Vector2.zero, CursorMode.Auto);
            transform.Translate(-diff * Time.deltaTime * moveSpeed);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        preMousePos = mousePos;
    }

    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}