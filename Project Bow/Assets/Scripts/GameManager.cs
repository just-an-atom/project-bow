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

    public bool vSyncOn;
    public Toggle vSyncToggle;

    public int graphicsPreset = 0;

    private bool changeToggle;

    private void Start() {
        changeToggle = false;
        LoadUserData();
        
        bloodToggle.isOn = blood;
        consoleToggle.isOn = consoleAllowed;
        fpsToggle.isOn = fpsOn;
        vSyncToggle.isOn = vSyncOn;

        FOVSlider.value = FOV;
        FOVInput.text = FOV.ToString();

        SensSlider.value = MouseSens;
        SensInput.text = MouseSens.ToString();
        changeToggle = true;
    }

    public void SaveUserData() {
        print("Saving...");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");

        ES3.Save<bool>("console", consoleAllowed, "user.settings", settings);
        ES3.Save<bool>("vsync", vSyncOn, "user.settings", settings);
        ES3.Save<bool>("blood", blood, "user.settings", settings);
        ES3.Save<bool>("FPS", fpsOn, "user.settings", settings);
        ES3.Save<float>("mouseSens", MouseSens, "user.settings", settings);
        ES3.Save<int>("graphicsPreset", graphicsPreset, "user.settings", settings);
    }

    public void LoadUserData() {
        print("Loading...");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");

        consoleAllowed = ES3.Load<bool>("console", "user.settings", false, settings);
        vSyncOn = ES3.Load<bool>("vsync", "user.settings", true, settings);
        blood = ES3.Load<bool>("blood", "user.settings", true, settings);
        fpsOn = ES3.Load<bool>("FPS", "user.settings", false, settings);
        MouseSens = ES3.Load<float>("mouseSens", "user.settings", 150, settings);
        graphicsPreset = ES3.Load<int>("graphicsPreset", "user.settings", 0, settings);
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

    public void TogglevSyncOn() {
        if(changeToggle == true) {
            vSyncOn = !vSyncOn;

            if (vSyncOn == true) {
                QualitySettings.vSyncCount = 1;
            } else {
                QualitySettings.vSyncCount = 0;
            }
        }
    }

    public void BloodFunc() {
        if(changeToggle == true) {
            blood = !blood;
        }
    }

    public void ConsoleFunc() {
        if(changeToggle == true) {
            consoleAllowed = !consoleAllowed;

            if (consoleAllowed == false) {
                isConsoleOpen = false;
                ConsoleObj.SetActive(isConsoleOpen);
            }
        }
    }

    public void FpsFunc() {
        if(changeToggle == true) {
            fpsOn = !fpsOn;
        }
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
        SaveUserData();
    }
}
