using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PlayerInteractManager : MonoBehaviour
{
    //temp for menu controls
    public GameObject gameMenuObj;
    private GameMenu gameMenu;
    //

    private Camera camera;

    private InventoryManager inventoryManager;

    int layerMaskInteract = 1 << 9;
    int layerMaskMoveableProp = 1 << 10;
    int layerMaskMP;

    float interactRange = 2.5f;

    private Interactable selectedGameObject;
    bool stillSelected = false;
    Transform lastSelectedHit = null;

    void Start()
    {
        layerMaskMP = LayerMask.NameToLayer("MoveableProp");

        //temp for menu controls
        gameMenu = gameMenuObj.GetComponent<GameMenu>();

        camera = gameObject.GetComponent<Camera>();
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //true when layer mask 9 (Interactable) is hit, else otherwise.
        if (Physics.Raycast(ray, out hit, interactRange, layerMaskInteract | layerMaskMoveableProp))
        {
            //detects if a new object is selected, without a gap (from object to object raycast, two object hugging eachother)
            if (hit.transform != lastSelectedHit && stillSelected)
            {
                stillSelected = false;
                selectedGameObject.OnRayExit();
            }
            //Player just now looked at an item
            if (!stillSelected)
            {
                selectedGameObject = hit.transform.gameObject.GetComponent<Interactable>();
                selectedGameObject.OnRay();
                //makes sure item is told only once that it is being looked at
                stillSelected = true;
                //needed to tell if player went to look at another item to another item without any other layer mask detected.
                lastSelectedHit = hit.transform;
            }
           

            if (Input.GetKeyDown(KeyCode.E))
            {
                //another raycast when key is hit to make sure target taken is not behind anything
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    
                    Transform objectHit = hit.transform;
                    if (objectHit.CompareTag("Item"))
                    {
                        stillSelected = false;
                        inventoryManager.TakeItem(objectHit);
                    }
                    else
                    {
                        Interactable obj = objectHit.gameObject.GetComponent<Interactable>();
                        obj.OnPress(1);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //another raycast when key is hit to make sure target taken is not behind anything
                if (Physics.Raycast(ray, out hit, interactRange))
                {

                    Transform objectHit = hit.transform;
                    if(layerMaskMP == objectHit.gameObject.layer)
                    {
                        Interactable obj = objectHit.gameObject.GetComponent<Interactable>();
                        obj.OnPress(-1);
                    }
                }
            }
        }
        else
        {
            //player is no longer looking at the last interactable
            if(stillSelected == true)
            {
                selectedGameObject.OnRayExit();
                stillSelected = false;
            }
        }
        //




        //TEMP NEED TO MOVE THIS SOMEWHERE ELSE, TEMP KEY L BECAUSE ESCAPE LEAVES EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(gameMenu.mainObj.activeSelf)
            {
                gameMenu.CloseMenu();
            }
            else
            {
                gameMenu.OpenMenu();
            }
        }


        //TEMP
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventoryManager.DropItem();
        }   
    }
}
