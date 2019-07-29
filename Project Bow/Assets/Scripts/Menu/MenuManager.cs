using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject LoadSlabItemPrefab;
    public Transform LoadSlabItemParent;
    public List<string> listOfSaves = new List<string>();

    public string saveName;
    public GameObject loadUI;

    private Storage storage;

    void Start()
    {
        GameObject storageObj = GameObject.FindGameObjectWithTag("Storage");
        storage = storageObj.GetComponent<Storage>();

        LoadSlots();
    }

    public void LoadSlots() {
       // var settings = new ES3Settings(ES3.EncryptionType.None, "");
        var encryptSettings = new ES3Settings(ES3.EncryptionType.AES, "Nan00kcj!");

        if (ES3.FileExists("user/slots.txt")) {
            listOfSaves = ES3.Load<List<string>>("listOfSaves", "user/slots.txt", encryptSettings);
            ES3.Save<List<string>>("listOfSaves", listOfSaves, "user/slots.txt", encryptSettings);
        }
        // } else {
        //     listOfSaves.Add(saveName);
        //     ES3.Save<List<string>>("listOfSaves", listOfSaves, "user/slots.txt", encryptSettings);
        //     ES3.Save<int>("levelID", 2, "user/"+saveName+"/data.dat", encryptSettings);
        //     StartCoroutine(snapShot());
        // }

        foreach (string slots in listOfSaves)
        {
            print(slots);
            GameObject LoadSlabItemGO = Instantiate(LoadSlabItemPrefab);
            LoadSlabItemGO.transform.SetParent(LoadSlabItemParent);

            SaveSlot saveSlot = LoadSlabItemGO.GetComponent<SaveSlot>();
            saveSlot.name = slots;
        }
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
        
        ES3.SaveImage(texture, "user/"+saveName+"/cover.jpg", encryptSettings);
        yield return null;
    }


    public void ShowLoadMenu() {
        loadUI.SetActive(true);
    }

    public void New() {
        var encryptSettings = new ES3Settings(ES3.EncryptionType.AES, "Nan00kcj!");
        
        storage.user = "Adam";

        listOfSaves.Add(storage.user);
        ES3.Save<List<string>>("listOfSaves", listOfSaves, "user/slots.txt", encryptSettings);
        ES3.Save<int>("levelID", 2, "user/"+storage.user+"/data.dat", encryptSettings);

        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
    }

    public void Exit() {
        Application.Quit();
    }
}
