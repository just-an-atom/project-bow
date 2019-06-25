using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovment : MonoBehaviour
{
    public Transform PlayerObject;
    public GameObject PlayerCameraView;
    public float MouseSensitivity;

    private float mousePosX;
    private float mousePosY;

    public float mouseMinimumX = -90F;
    public float mouseMaximumX = 90F;

    private float rotationX = 0F;

    // Just some startup things
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {

        // Get mousePos X, Y
        mousePosX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;
        mousePosY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;

        // Limit Y Rotation
        rotationX += Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, mouseMinimumX, mouseMaximumX);
        PlayerCameraView.transform.localEulerAngles = new Vector3(-rotationX, 0, 0);

        // Set PlayerObject and PlayerCameraView Rotation
        PlayerCameraView.transform.Rotate(-mousePosY, 0, 0);
        PlayerObject.transform.Rotate(0, mousePosX, 0);
    }
}
