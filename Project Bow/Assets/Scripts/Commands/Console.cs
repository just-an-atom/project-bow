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
            return;
        } else if (input == "credits" || input == "credit") {
            command.Credit();
            return;
        } if (input == "restart") {
            command.Restart();
            return;
        } else if (input == "respawn") {
            command.Respawn();
            return;
        } else if(input == "ray") {
            command.Ray();
            return;
        } else if(input == "fps") {
            command.ToggleFPS();
            return;
        } else if(input == "save") {
            command.Save();
            return;
        } else if(input == "load") {
            command.Load();
            return;
        } else if(input == "debug") {
            command.Debugger();
            return;
        } else if(input == "main_menu") {
            command.MainMenu();
            return;
        } else if(input == "") {
            return;
        } else {
            command.LogToConsole("Unknown command.");
        }

        inputField.text = "";
    }
}
