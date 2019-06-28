using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Command : MonoBehaviour
{
    private GameManager gameManager;

    public TMP_InputField inputField;
    public Transform OutputParent;
    public GameObject OutputPrefab;

    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = this.GetComponent<GameManager>();
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
