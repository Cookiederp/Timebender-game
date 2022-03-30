using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject gameMenuObj;
    private GameMenu gameMenu;
    private CamController camController;
    [HideInInspector]
    public UIInteractManager uiInteractManager;
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
        gameMenu = FindObjectOfType<GameMenu>();
        gameMenuObj = gameMenu.gameObject;
        uiInteractManager = FindObjectOfType<UIInteractManager>();
        camController = FindObjectOfType<CamController>();
    }

    void Update()
    {
        //TEMP KEY L BECAUSE ESCAPE LEAVES EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (gameMenu.mainObj.activeSelf)
            {
                gameMenu.CloseMenu();
            }
            else
            {
                gameMenu.OpenMenu();
            }
        }
    }


    //this gets called from in class and close settings.
    public void ApplySettings()
    {
        StartCoroutine(waitAFrame());
    }

    IEnumerator waitAFrame()
    {
        yield return new WaitForEndOfFrame();
        camController.UpdateMouseSens();
        yield return null;
    }
}
