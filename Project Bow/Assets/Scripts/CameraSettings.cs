using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    private GameManager gameManager;

    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    private void Update() {
        cam.fieldOfView = gameManager.FOV;
    }
}
