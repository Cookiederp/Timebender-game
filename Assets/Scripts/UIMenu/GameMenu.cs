using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject mainObj;
    public GameObject settingMenuObj;
    public GameObject crosshair;


    private void Start()
    {
        crosshair.SetActive(true);
        mainObj.SetActive(false);
        settingMenuObj.SetActive(false);
    }

    private void OnEnable()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        crosshair.SetActive(true);
        mainObj.SetActive(false);
        settingMenuObj.SetActive(false);
    }

    public void OnResume()
    {
        mainObj.SetActive(false);
    }

    public void OnSettings()
    {
        settingMenuObj.SetActive(true);
    }

    //Change this later save cur level with playerpref
    public void OnQuit()
    {
        SceneManager.LoadScene(0);
    }
}
