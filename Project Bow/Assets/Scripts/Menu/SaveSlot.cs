using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    public Storage storage;

    public string name;
    public TextMeshProUGUI nameTMP;

    public int levelID;
    public RawImage cover;

    private void Start() {
        GameObject storageObj = GameObject.FindGameObjectWithTag("Storage");
        storage = storageObj.GetComponent<Storage>();

        nameTMP.text = name;
        LoadSave();
    }

    public void LoadSave() {
       // var settings = new ES3Settings(ES3.EncryptionType.None, "");
        var encryptSettings = new ES3Settings(ES3.EncryptionType.AES, "Nan00kcj!");

        levelID = ES3.Load<int>("levelID", "user/"+name+"/data.dat", 2, encryptSettings);
        print(levelID);

        var texture = ES3.LoadImage("user/"+name+"/cover.jpg", encryptSettings);
        cover.texture = texture;
    }

    public void SlotClick() {
        storage.user = name;
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelID);
    }
}
