using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovment : MonoBehaviour
{
    public GameManager gameManager;

    public Transform PlayerObject;
    public GameObject PlayerCameraView;

    private float mousePosX;
    private float mousePosY;

    public float mouseMinimumX = -90F;
    public float mouseMaximumX = 90F;

    private float rotationX = 0F;

    // Just some startup things
    private void Start()
    {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        // Get mousePos X, Y
        mousePosX = Input.GetAxis("Mouse X") * Time.deltaTime * gameManager.MouseSens;
        mousePosY = Input.GetAxis("Mouse Y") * Time.deltaTime * gameManager.MouseSens;

        // Limit Y Rotation
        rotationX += Input.GetAxis("Mouse Y") * Time.deltaTime * gameManager.MouseSens;
        rotationX = Mathf.Clamp(rotationX, mouseMinimumX, mouseMaximumX);
        PlayerCameraView.transform.localEulerAngles = new Vector3(-rotationX, 0, 0);

        // Set PlayerObject and PlayerCameraView Rotation
        PlayerCameraView.transform.Rotate(-mousePosY, 0, 0);
        PlayerObject.transform.Rotate(0, mousePosX, 0);
    }
}
