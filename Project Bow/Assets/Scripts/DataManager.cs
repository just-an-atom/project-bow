using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() {
        gameManager = this.GetComponent<GameManager>();
    }

    public void SaveUserData() {
        print("Saving...");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        ES3.Save<bool>("console", gameManager.consoleAllowed, "user.settings", settings);
        ES3.Save<bool>("vsync", gameManager.vSyncOn, "user.settings", settings);
        ES3.Save<bool>("blood", gameManager.blood, "user.settings", settings);
        ES3.Save<bool>("FPS", gameManager.fpsOn, "user.settings", settings);
        ES3.Save<float>("mouseSens", gameManager.MouseSens, "user.settings", settings);
        ES3.Save<int>("graphicsPreset", gameManager.graphicsPreset, "user.settings", settings);
    }

    public void SaveUserDataDebug(string path) {
        print("Saving Debug File...");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        ES3.Save<bool>("console", gameManager.consoleAllowed, path+"user.settings", settings);
        ES3.Save<bool>("vsync", gameManager.vSyncOn, path+"user.settings", settings);
        ES3.Save<bool>("blood", gameManager.blood, path+"user.settings", settings);
        ES3.Save<bool>("FPS", gameManager.fpsOn, path+"user.settings", settings);
        ES3.Save<float>("mouseSens", gameManager.MouseSens, path+"user.settings", settings);
        ES3.Save<int>("graphicsPreset", gameManager.graphicsPreset, path+"user.settings", settings);
    }

    public void LoadUserData() {
        print("Loading...");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        gameManager.consoleAllowed = ES3.Load<bool>("console", "user.settings", false, settings);
        gameManager.vSyncOn = ES3.Load<bool>("vsync", "user.settings", true, settings);
        gameManager.blood = ES3.Load<bool>("blood", "user.settings", true, settings);
        gameManager.fpsOn = ES3.Load<bool>("FPS", "user.settings", false, settings);
        gameManager.MouseSens = ES3.Load<float>("mouseSens", "user.settings", 150, settings);
        gameManager.graphicsPreset = ES3.Load<int>("graphicsPreset", "user.settings", 0, settings);
    }
}
