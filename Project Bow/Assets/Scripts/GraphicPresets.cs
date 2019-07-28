using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// using UnityEngine.Rendering.PostProcessing;

public class GraphicPresets : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_Dropdown PresetsDropdown;
    public int ListID;
    public string[] names;

    private void Start() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        names = QualitySettings.names;
        
        QualitySettings.SetQualityLevel(gameManager.graphicsPreset);
        PresetsDropdown.value = gameManager.graphicsPreset;
    }

    public void GetDropdownListID() {
        ListID = PresetsDropdown.value;
        QualitySettings.SetQualityLevel(ListID, true);
        gameManager.graphicsPreset = ListID;
        gameManager.SaveUserData();
    }
}
