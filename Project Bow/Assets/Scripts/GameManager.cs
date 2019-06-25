using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isConsoleOpen = false;
    public GameObject ConsoleObj;

    public bool isADS = false;

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
    }
}
