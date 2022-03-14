using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PlayerInteractManager : MonoBehaviour
{
    private Camera camera;

    private InventoryManager inventoryManager;

    int layerMaskInteractableMoveable = 1 << 9;
    int layerMaskInteractableStatic = 1 << 10;
    int layerMaskDefault = 1 << 0;
    int layerMaskStatic;
    int layerMaskMoveable;
    int layerMaskDef;

    float interactRange = 3f;

    private Interactable selectedGameObject;
    bool stillSelected = false;
    Transform lastSelectedHit = null;

    void Start()
    {
        layerMaskMoveable = LayerMask.NameToLayer("InteractableMoveable");
        layerMaskStatic = LayerMask.NameToLayer("InteractableStatic");
        layerMaskDef = LayerMask.NameToLayer("Default");

        camera = Camera.main;
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactRange, layerMaskInteractableStatic | layerMaskInteractableMoveable | layerMaskDefault))
        {
            Transform objectHit = hit.transform;
            //detects if a new object is selected, without a gap (from object to object raycast, two object hugging eachother)
            if (objectHit.gameObject.layer == layerMaskDef)
            {
                //player is no longer looking at the last interactable
                if (stillSelected == true)
                {
                    selectedGameObject.OnRayExit();
                    stillSelected = false;
                }
            }
            else
            {
                if (objectHit != lastSelectedHit && stillSelected)
                {
                    stillSelected = false;
                    selectedGameObject.OnRayExit();
                }
                //Player just now looked at an item
                if (!stillSelected)
                {
                    if (objectHit.CompareTag("Pickup")){
                        selectedGameObject = objectHit.gameObject.GetComponent<ItemObject>();
                    }
                    else
                    {
                        selectedGameObject = objectHit.gameObject.GetComponent<Interactable>();
                    }
                    selectedGameObject.OnRay();
                    //makes sure item is told only once that it is being looked at
                    stillSelected = true;
                    //needed to tell if player went to look at another item to another item without any other layer mask detected.
                    lastSelectedHit = hit.transform;
                }
            }
        }
        else
        {
            //player is no longer looking at the last interactable
            if (stillSelected == true)
            {
                selectedGameObject.OnRayExit();
                stillSelected = false;
            }
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.F))
        {
            //another raycast when key is hit to make sure target taken is not behind anything
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                Transform objectHit = hit.transform;
                int hitLayer = objectHit.gameObject.layer;
                if (layerMaskStatic == hitLayer || layerMaskMoveable == hitLayer)
                {
                    if (objectHit.CompareTag("Pickup"))
                    {
                        stillSelected = false;
                        inventoryManager.TakeItem(objectHit);
                    }
                    else
                    {
                        if(layerMaskStatic == hitLayer)
                        {
                            Interactable obj = objectHit.gameObject.GetComponent<Interactable>();
                            obj.OnPress(1);
                        }
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //another raycast when key is hit to make sure target taken is not behind anything
            if (Physics.Raycast(ray, out hit, interactRange))
            {                    
                Transform objectHit = hit.transform;
                if (layerMaskMoveable == objectHit.gameObject.layer)
                {
                    if (!objectHit.CompareTag("Pickup"))
                    {
                        Interactable obj = objectHit.gameObject.GetComponent<Interactable>();
                        obj.OnPress(1);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //another raycast when key is hit to make sure target taken is not behind anything
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                Transform objectHit = hit.transform;
                if (layerMaskMoveable == objectHit.gameObject.layer)
                {
                    if (!objectHit.CompareTag("Pickup"))
                    {
                        Interactable obj = objectHit.gameObject.GetComponent<Interactable>();
                        obj.OnPress(-1);
                    }
                }
            }
        }

        //TEMP
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventoryManager.DropItem();
        }   
    }
}
