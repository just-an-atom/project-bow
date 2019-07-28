using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Command : MonoBehaviour
{
    private GameManager gameManager;
    private DataManager dataManager;

    public TMP_InputField inputField;
    public Transform OutputParent;
    public GameObject OutputPrefab;
    public GameObject hud;

    private Transform player;
    private string date = System.DateTime.Now.ToString("yyyy-MM-dd-fff");

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = this.GetComponent<GameManager>();
        dataManager = this.GetComponent<DataManager>();
    }

    // Feedback text for console
    public void LogToConsole(string message) {
        GameObject outputObj = Instantiate(OutputPrefab);
        outputObj.transform.SetParent(OutputParent, false);
        TextMeshProUGUI OutputText = outputObj.GetComponent<TextMeshProUGUI>();

        OutputText.text = message;
        inputField.text = "";
    }

    public void Exit() {
        LogToConsole("Quitting...");
        Application.Quit();
    }

    public void Credit() {
        LogToConsole("Coded with ♥ by Adam Jackson");
    }

    public void Restart() {
        LogToConsole("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Respawn() {
        LogToConsole("Respawning...");
        player.transform.position = new Vector3(0, 3, 0);
    }

    public void Ray() {
        LogToConsole("Getting object info");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(player.position, player.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(player.position, player.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            LogToConsole("Hit:: " + hit.transform.name);
        }
    }

    public void Save() {
        dataManager.SaveUserData();
        LogToConsole("Saved file.");
    }

    public void Load() {
        dataManager.LoadUserData();
        LogToConsole("Loaded last known save.");
    }

    public void Debugger() {
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        string debugDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");

        ES3.Save<Vector3>("player_position", player.position, "debug/"+date+"/report.json", settings);
        ES3.Save<String>("Date", debugDate, "debug/"+date+"/report.json", settings);
        ES3.Save<String>("operatingSystem", SystemInfo.operatingSystem, "debug/"+date+"/report.json", settings);
        ES3.Save<String>("graphicsDeviceName", SystemInfo.graphicsDeviceName, "debug/"+date+"/report.json", settings);
        ES3.Save<int>("graphicsMemorySize", SystemInfo.graphicsMemorySize, "debug/"+date+"/report.json", settings);
        ES3.Save<String>("processorType", SystemInfo.processorType, "debug/"+date+"/report.json", settings);
        ES3.Save<int>("systemMemorySize", SystemInfo.systemMemorySize, "debug/"+date+"/report.json", settings);
        dataManager.SaveUserDataDebug("debug/"+date+"/savefile/");

        StartCoroutine(snapShot());
    }

    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    IEnumerator snapShot()
    {
        var settings = new ES3Settings(ES3.EncryptionType.None, "");

        gameManager.Unpause();
        hud.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        yield return frameEnd;

        // Take a screenshot.
        var texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        
        ES3.SaveImage(texture, "debug/"+date+"/screenshot.jpg", settings);
        hud.SetActive(true);
        gameManager.Pause();

        LogToConsole("Debug folder \""+date+"\" made.");
    }

    public void ToggleFPS() {
        gameManager.FpsFunc();
        bool fps = gameManager.fpsOn;
        gameManager.fpsToggle.isOn = !gameManager.fpsToggle.isOn;
        gameManager.FpsFunc();

        if (fps == true) {
            LogToConsole("FPS Display  ON");
        } else {
            LogToConsole("FPS Display  OFF");
        }
    }
}
