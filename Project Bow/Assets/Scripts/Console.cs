using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public TMP_InputField inputField;
    public Transform OutputParent;
    public GameObject OutputPrefab;
    
    public void Exec() {
        if (inputField.text == "exit") {
            Exit();
        } else if (inputField.text == "credits" || inputField.text == "credit") {
            Credits();
        } else {
            LogToConsole("Unknown command.");
        }
    }

    private void Exit() {
        LogToConsole("Quitting...");
        Application.Quit();
    }

    private void Credits() {
        LogToConsole("Coded with ♥ by Adam Jackson");
    }

    private void LogToConsole(string message) {
        GameObject outputObj = Instantiate(OutputPrefab);
        outputObj.transform.SetParent(OutputParent, false);
        TextMeshProUGUI OutputText = outputObj.GetComponent<TextMeshProUGUI>();

        OutputText.text = message;
        inputField.text = "";
    }
    
}
