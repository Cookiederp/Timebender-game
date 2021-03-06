using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PlayerInteractManager : MonoBehaviour
{
    private Camera camera;

    private InventoryManager inventoryManager;
    public GameObject spellTimeTravelPropsObj;

    int layerMaskInteractableMoveable = 1 << 9;
    int layerMaskInteractableStatic = 1 << 10;
    int layerMaskRagdollTime = 1 << 12;
    int layerIgnoreRaycast = 1 << 2;
    int layerMaskDefault = 1 << 0;
    int layerMaskStatic;
    int layerMaskMoveable;
    int layerMaskDef;

    int layerMaskRag;
    int layerMaskRagTime = 1 << 12;

    float interactRange = 3.5f;

    private Interactable selectedGameObject;
    bool stillSelected = false;
    bool itemRefreshTimeReceiverOnly = false;
    Transform lastSelectedHit = null;

    void Start()
    {
        layerMaskMoveable = LayerMask.NameToLayer("InteractableMoveable");
        layerMaskStatic = LayerMask.NameToLayer("InteractableStatic");
        layerMaskDef = LayerMask.NameToLayer("Default");
        layerMaskRag = LayerMask.NameToLayer("InteractableRagdollTime");

        camera = Camera.main;

        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    public void UpdateTimeText()
    {
        if(selectedGameObject != null)
        {
            if(selectedGameObject.gameObject.layer == layerMaskMoveable || selectedGameObject.gameObject.layer == layerMaskRag)
            {
                if (selectedGameObject.CompareTag("Pickup")){
                    itemRefreshTimeReceiverOnly = true;
                    stillSelected = false;
                }
                else
                {
                    stillSelected = false;
                }
            }
        }
    }

    //when a player looks at an interactable, call OnRay() and OnRayExit() on the interactable.
    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactRange, layerMaskInteractableStatic | layerMaskInteractableMoveable | layerMaskDefault | layerMaskRagTime | (~layerMaskRagdollTime & ~layerIgnoreRaycast)))
        {
            Transform objectHit = hit.transform;
            //detects if a new object is selected, without a gap (from object to object raycast, two object hugging eachother)
            if (objectHit.gameObject.layer == layerMaskDef)
            {
                //player is no longer looking at the last interactable
                if (stillSelected == true)
                {
                    if (selectedGameObject != null)
                    {
                        selectedGameObject.OnRayExit();
                        stillSelected = false;
                    }
                }
            }
            else
            {
                if (objectHit != lastSelectedHit && stillSelected)
                {
                    stillSelected = false;
                    if (selectedGameObject!=null)
                        selectedGameObject.OnRayExit();
                }
                //Player just now looked at an item
                if (!stillSelected)
                {
                    if (objectHit.CompareTag("Pickup")){
                        selectedGameObject = objectHit.gameObject.GetComponent<ItemObject>();

                        if (itemRefreshTimeReceiverOnly)
                        {
                            itemRefreshTimeReceiverOnly = false;
                            selectedGameObject.OnRay(true);
                        }
                        else
                        {
                            selectedGameObject.OnRay();
                        }
                    }
                    else
                    {
                        if(layerMaskStatic == objectHit.gameObject.layer)
                        {
                            //puzzle objects
                            selectedGameObject = objectHit.gameObject.GetComponent<InteractablePuzzle>();
                        }
                        else
                        {                           
                            selectedGameObject = objectHit.gameObject.GetComponent<TimeTravelReceiver>();
                        }

                        if (selectedGameObject != null)
                        {
                            selectedGameObject.OnRay();
                        }
                    }


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
                if (selectedGameObject != null)
                {
                    selectedGameObject.OnRayExit();
                    stillSelected = false;
                }

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
            if (Physics.Raycast(ray, out hit, interactRange, ~layerMaskRagdollTime & ~layerIgnoreRaycast))
            {
                Transform objectHit = hit.transform;
                int hitLayer = objectHit.gameObject.layer;
                if (layerMaskStatic == hitLayer || layerMaskMoveable == hitLayer)
                {
                    if (objectHit.CompareTag("Pickup"))
                    {
                        //pickups
                        stillSelected = false;
                        inventoryManager.TakeItem(objectHit);
                    }
                    else
                    {
                        if(layerMaskStatic == hitLayer)
                        {
                            //puzzle objects
                            Interactable obj = objectHit.gameObject.GetComponent<InteractablePuzzle>();
                            if(obj != null)
                                obj.OnPress(1);
                        }
                    }
                }
            }
        }      
    }
}
