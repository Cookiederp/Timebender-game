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

    public void OpenMenu()
    {
        mainObj.SetActive(true);
        Time.timeScale = 0;
        if (settingMenuObj.activeSelf)
        {
            settingMenuObj.GetComponent<SettingsMenu>().OnBack();
        }
        settingMenuObj.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        mainObj.SetActive(false);
        settingMenuObj.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
    }

    public void OnResume()
    {
        CloseMenu();
    }

    public void OnSettings()
    {
        settingMenuObj.SetActive(true);
    }

    //Change this later save cur level with playerpref
    public void OnQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
