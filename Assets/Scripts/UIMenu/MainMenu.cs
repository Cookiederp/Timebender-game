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
    public GameObject CreditsMenuObj;

    public AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loadScene = gameObject.GetComponent<LoadScene>();
        MainObj.SetActive(true);
        SettingsMenuObj.SetActive(false);
        CreditsMenuObj.SetActive(false);
    }

    //MMenu
    public void OnStart()
    {
        loadScene.Load();
        audioSource.Play();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
    //

    

}
