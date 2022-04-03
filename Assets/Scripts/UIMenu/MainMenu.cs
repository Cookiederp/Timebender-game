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
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
