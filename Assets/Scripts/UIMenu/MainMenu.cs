using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    LoadScene loadScene;
    public GameObject MainObj;

    public GameObject SettingsMenuObj;

    private void Start()
    {
        loadScene = gameObject.GetComponent<LoadScene>();
        MainObj.SetActive(true);
        SettingsMenuObj.SetActive(false);
    }

    //MMenu
    public void OnStart()
    {
        loadScene.Load();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
    //

    

}
