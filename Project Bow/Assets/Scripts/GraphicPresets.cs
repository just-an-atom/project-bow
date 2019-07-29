using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// using UnityEngine.Rendering.PostProcessing;

public class GraphicPresets : MonoBehaviour
{
    private GameManager gameManager;
    private DataManager dataManager;

    public TMP_Dropdown PresetsDropdown;
    public int ListID;
    public string[] names;

    private void Start() {
        var settings = new ES3Settings(ES3.EncryptionType.None, "");
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        dataManager = gameManagerObj.GetComponent<DataManager>();

        names = QualitySettings.names;
        
        ListID = ES3.Load<int>("graphicsPreset", "user.settings", 0, settings);

        QualitySettings.SetQualityLevel(ListID);
        PresetsDropdown.value = ListID;
    }

    public void GetDropdownListID() {
        ListID = PresetsDropdown.value;
        QualitySettings.SetQualityLevel(ListID, true);
        gameManager.graphicsPreset = ListID;
    }
}
