using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isConsoleOpen = false;
    public GameObject ConsoleObj;
    public float FOV = 90;

    public bool isPaused = false;
    public bool isADS = false;

    public GameObject PauseMenu;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            isConsoleOpen =! isConsoleOpen;
            ConsoleObj.SetActive(isConsoleOpen);

            if (isConsoleOpen == true) {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            Cursor.visible = isConsoleOpen;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused = !isPaused;

            if (isPaused) {
                // When Paused
                Pause();
            } else {
                // When Unpaused
                Unpause();
            }
        }
    }

    private void Pause() {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Unpause() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
