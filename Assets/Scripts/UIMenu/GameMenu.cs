using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject mainObj;
    public GameObject settingMenuObj;
    public GameObject crosshair;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
        crosshair.SetActive(true);
        mainObj.SetActive(false);
        settingMenuObj.SetActive(false);
    }

    public void OpenMenu()
    {
        gameManager.pauseGame();
        mainObj.SetActive(true);
        if (settingMenuObj.activeSelf)
        {
            gameObject.GetComponent<SettingsMenu>().OnBack();
        }
        settingMenuObj.SetActive(false);
    }

    public void CloseMenu()
    {
        gameManager.unPauseGame();
        mainObj.SetActive(false);
        settingMenuObj.SetActive(false);
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
