using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject gameMenuObj;
    private GameMenu gameMenu;
    private CamController camController;
    [HideInInspector]
    public bool isGamePaused
    {
        get { return isGamePaused_; }
    }
    private bool isGamePaused_;

    public bool isNextSceneLoading
    {
        get { return isNextSceneLoading_; }
    }
    private bool isNextSceneLoading_;

    [HideInInspector]
    public bool isSpellTimeTravelActive = false;
    [HideInInspector]
    public UIInteractManager uiInteractManager;
    [HideInInspector]
    public InventoryManager inventoryManager;
    [HideInInspector]
    public PlayerHealthManager playerHealthManager;
    //singleton
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    //

    private void Awake()
    {
        Time.timeScale = 1;
        gameMenu = FindObjectOfType<GameMenu>();
        gameMenuObj = gameMenu.gameObject;
        uiInteractManager = FindObjectOfType<UIInteractManager>();
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        camController = FindObjectOfType<CamController>();
        AudioListener.volume = PlayerPrefs.GetFloat("sound", 0.5f);
    }

    void Update()
    {
        //TEMP KEY L BECAUSE ESCAPE LEAVES EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isNextSceneLoading)
            {
                if (gameMenu.mainObj.activeSelf)
                {
                    gameMenu.CloseMenu();
                }
                else
                {
                    if (gameMenu.settingMenuObj.activeSelf)
                    {
                        ApplySettings();
                    }

                    gameMenu.OpenMenu();
                }
            }
        }
    }

    public void pauseGame()
    {
        isGamePaused_ = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void unPauseGame()
    {
        isGamePaused_ = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //this gets called from in class and close settings.
    public void ApplySettings()
    {
        StartCoroutine(WaitAFrame());
    }

    IEnumerator WaitAFrame()
    {
        yield return new WaitForEndOfFrame();
        camController.UpdateMouseSens();
        yield return null;
    }

    public void OnLoad()
    {
        isNextSceneLoading_ = true;
    }
}
