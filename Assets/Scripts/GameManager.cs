using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject gameMenuObj;
    private GameMenu gameMenu;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        //temp for menu controls
        gameMenu = gameMenuObj.GetComponent<GameMenu>();
    }


    void Update()
    {
        //TEMP NEED TO MOVE THIS SOMEWHERE ELSE, TEMP KEY L BECAUSE ESCAPE LEAVES EDITOR
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
}
