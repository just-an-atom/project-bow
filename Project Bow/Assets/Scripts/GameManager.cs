using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public Toggle consoleToggle;
    public bool consoleAllowed;
    public bool isConsoleOpen = false;
    public GameObject ConsoleObj;
    
    public float FOV = 90;
    public Slider FOVSlider;
    public TMP_InputField FOVInput;

    // min 0 max 2 0.15 feels right
    public float MouseSens = 150;
    public Slider SensSlider;
    public TMP_InputField SensInput;

    public bool isPaused = false;
    public bool isADS = false;
    public bool isCrouched = false;
    public bool canShoot = true;

    // blood and gore bool
    public bool blood;
    public Toggle bloodToggle;

    public GameObject PauseMenu;
    public GameObject SettingsMenuObj;

    public bool fpsOn;
    public Toggle fpsToggle;

    private void Awake() {
        bloodToggle.isOn = blood;
        consoleToggle.isOn = consoleAllowed;
        fpsToggle.isOn = fpsOn;
    }

    private void Start() {
        FOVSlider.value = FOV;
        FOVInput.text = FOV.ToString();

        SensSlider.value = MouseSens;
        SensInput.text = MouseSens.ToString();

    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            if(consoleAllowed == true) {
                isConsoleOpen =! isConsoleOpen;
                ConsoleObj.SetActive(isConsoleOpen);

                if (isConsoleOpen == true) {
                    Pause();
                } else
                {
                    Unpause();
                }

                Cursor.visible = isConsoleOpen;
            }
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

    public void BloodFunc() {
        // Inverts bool value
        blood = !blood;
    }

    public void ConsoleFunc() {
        // Inverts bool value
        consoleAllowed = !consoleAllowed;

        if (consoleAllowed == false) {
            isConsoleOpen = false;
            ConsoleObj.SetActive(isConsoleOpen);
        }
    }

    public void FpsFunc() {
        // Inverts bool value
        fpsOn = !fpsOn;
    }

    // Saving space xD
    public void MouseSensFunc() {
        MouseSens = SensSlider.value;
        SensInput.text = MouseSens.ToString();
    }

    public void SetMouseSensFunc() {
        float RawFloatSens = float.Parse(SensInput.text);

        if (RawFloatSens > SensSlider.maxValue) {
            MouseSens = SensSlider.maxValue;
        } else if (RawFloatSens < SensSlider.minValue) {
            MouseSens = SensSlider.minValue;
        } else {
            MouseSens = RawFloatSens;
        }
        SensSlider.value = MouseSens;
        SensInput.text = MouseSens.ToString();
    }

    public void FOVFunc() {
        FOV = FOVSlider.value;
        FOVInput.text = FOV.ToString();
    }

    public void SetFOVFunc() {
        float RawFloatInput = float.Parse(FOVInput.text);

        if (RawFloatInput > 179) {
            FOV = 179;
        } else if (RawFloatInput < 1) {
            FOV = 1;
        } else {
            FOV = RawFloatInput;
        }
        FOVSlider.value = FOV;
        FOVInput.text = FOV.ToString();
    }

    public void SettingsMenu(bool state) {
        SettingsMenuObj.SetActive(state);
    }

    public void Pause() {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canShoot = false;
    }

    public void Unpause() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        isConsoleOpen = false;
        Cursor.visible = false;
        ConsoleObj.SetActive(isConsoleOpen);
        SettingsMenuObj.SetActive(false);
        canShoot = true;
    }
}
