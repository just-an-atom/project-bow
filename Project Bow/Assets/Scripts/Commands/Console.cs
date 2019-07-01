using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    public TMP_InputField inputField;
    public Command command;
    
    private void Start() {
        command = this.GetComponent<Command>();
        command.inputField = inputField;
    }

    public void Exec() {
        string input = inputField.text.ToLower();

        if (input == "exit" || input == "close" || input == "quit") {
            command.Exit();
        } else if (input == "credits" || input == "credit") {
            command.Credit();
        } if (input == "restart") {
            command.Restart();
        } else if (input == "respawn") {
            command.Respawn();
        } else if(input == "ray") {
            command.Ray();
        } else if(input == "fps") {
            command.ToggleFPS();
        } else if(input == "") {
            return;
        } else {
            command.LogToConsole("Unknown command.");
        }
    }
}
