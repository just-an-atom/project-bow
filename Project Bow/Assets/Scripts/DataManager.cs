using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public GameManager gameManager;
    public Storage storage;

    private void Start() {
        GameObject storageObj = GameObject.FindGameObjectWithTag("Storage");
        storage = storageObj.GetComponent<Storage>();

        gameManager = this.GetComponent<GameManager>();
    }

    public void SaveUserData() {
        print("Saving...");

        // user.settings
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        var encryptSettings = new ES3Settings(ES3.EncryptionType.AES, "Nan00kcj!");
        
        // stored in user.settings
        ES3.Save<bool>("console", gameManager.consoleAllowed, "user.settings", settings);
        ES3.Save<bool>("vsync", gameManager.vSyncOn, "user.settings", settings);
        ES3.Save<bool>("blood", gameManager.blood, "user.settings", settings);
        ES3.Save<bool>("FPS", gameManager.fpsOn, "user.settings", settings);
        ES3.Save<float>("mouseSens", gameManager.MouseSens, "user.settings", settings);
        ES3.Save<float>("fov", gameManager.FOV, "user.settings", settings);
        ES3.Save<int>("graphicsPreset", gameManager.graphicsPreset, "user.settings", settings);

        // stored in user/NAME/data.dat
        ES3.Save<int>("levelID", SceneManager.GetActiveScene().buildIndex, "user/"+storage.user+"/data.dat", encryptSettings);
    }

    // files save to debug/DATE/savefile/
    public void SaveUserDataDebug(string path) {
        print("Saving Debug File...");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        
        ES3.Save<bool>("console", gameManager.consoleAllowed, path+"user.settings", settings);
        ES3.Save<bool>("vsync", gameManager.vSyncOn, path+"user.settings", settings);
        ES3.Save<bool>("blood", gameManager.blood, path+"user.settings", settings);
        ES3.Save<bool>("FPS", gameManager.fpsOn, path+"user.settings", settings);
        ES3.Save<float>("mouseSens", gameManager.MouseSens, path+"user.settings", settings);
        ES3.Save<float>("fov", gameManager.MouseSens, path+"user.settings", settings);
        ES3.Save<int>("graphicsPreset", gameManager.graphicsPreset, path+"user.settings", settings);
        ES3.Save<int>("levelID", SceneManager.GetActiveScene().buildIndex, path+"data.dat", settings);
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
