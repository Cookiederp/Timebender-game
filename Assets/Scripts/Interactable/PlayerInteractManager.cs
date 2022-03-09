using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PlayerInteractManager : MonoBehaviour
{
    //temp
    public GameObject gameMenuObj;
    //

    private Camera camera;

    private InventoryManager inventoryManager;

    int layerMaskInteract = 1 << 9;

    private Interactable selectedGameObject;
    bool stillSelected = false;
    Transform lastSelectedHit = null;

    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //true when layer mask 9 (Interactable) is hit, else otherwise.
        if (Physics.Raycast(ray, out hit, 2.5f, layerMaskInteract))
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
                if (Physics.Raycast(ray, out hit, 2.5f))
                {
                    Transform objectHit = hit.transform;
                    if (objectHit.CompareTag("Item"))
                    {
                        stillSelected = false;
                        inventoryManager.TakeItem(objectHit);
                    }
                    else if (objectHit.CompareTag("InteractableNonItem"))
                    {
                        Interactable obj = objectHit.gameObject.GetComponent<Interactable>();
                        obj.OnPress();
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

        //TEMP NEED TO MOVE THIS SOMEWHERE ELSE, TEMP KEY L BECAUSE ESCAPE LEAVES EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(gameMenuObj.activeSelf)
            {
                gameMenuObj.SetActive(false);
            }
            else
            {
                gameMenuObj.SetActive(true);
            }
        }


        //TEMP
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventoryManager.DropItem();
        }   
    }
}
