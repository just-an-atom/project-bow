using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public GameManager gameManager;
    public Storage storage;
    
    public Transform player;

    private void Start() {
        GameObject storageObj = GameObject.FindGameObjectWithTag("Storage");
        storage = storageObj.GetComponent<Storage>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        gameManager = this.GetComponent<GameManager>();
    }

    public void SaveUserData() {
        //print("Saved");

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
        ES3.Save<Vector3>("position", player.position, "user/"+storage.user+"/data.dat", encryptSettings);
        StartCoroutine(snapShot());
    }

    // Takes screenshot
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    IEnumerator snapShot()
    {
       // var settings = new ES3Settings(ES3.EncryptionType.None, "");
        var encryptSettings = new ES3Settings(ES3.EncryptionType.AES, "Nan00kcj!");

        yield return frameEnd;
        // Take a screenshot.
        var texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        
        ES3.SaveImage(texture, "user/"+storage.user+"/cover.jpg", encryptSettings);
        yield return null;
    }

    // files save to debug/DATE/savefile/
    public void SaveUserDataDebug(string path) {
        //print("Saving Debug File...");
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
        //print("Loaded");
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        var encryptSettings = new ES3Settings(ES3.EncryptionType.AES, "Nan00kcj!");

        gameManager.consoleAllowed = ES3.Load<bool>("console", "user.settings", false, settings);
        gameManager.vSyncOn = ES3.Load<bool>("vsync", "user.settings", true, settings);
        gameManager.blood = ES3.Load<bool>("blood", "user.settings", true, settings);
        gameManager.fpsOn = ES3.Load<bool>("FPS", "user.settings", false, settings);
        gameManager.MouseSens = ES3.Load<float>("mouseSens", "user.settings", 200, settings);
        gameManager.graphicsPreset = ES3.Load<int>("graphicsPreset", "user.settings", 0, settings);
        player.position = ES3.Load<Vector3>("position", "user/"+storage.user+"/data.dat", encryptSettings);
    }
}
