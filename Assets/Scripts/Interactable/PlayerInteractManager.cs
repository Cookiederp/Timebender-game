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

    //temp for grab Prop spell controls
    private float defaultRange;
    private float minRange = 2.3f;
    private float maxRange = 4.1f;

    public Transform holdLocation;
    private GameObject selectedProp;
    private Rigidbody selectedPropRb;
    //

    private Camera camera;

    private InventoryManager inventoryManager;

    int layerMaskInteract = 1 << 9;
    int layerMaskMoveableProp = 1 << 10;

    private Interactable selectedGameObject;
    bool stillSelected = false;
    Transform lastSelectedHit = null;

    void Start()
    {
        //temp for spell
        defaultRange = holdLocation.localPosition.z;

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
        //


        //temp spell, need own class
        //
        //control selected prop, move it away or closer to player
        if(Input.mouseScrollDelta.y != 0)
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                if (!(holdLocation.localPosition.z >= maxRange))
                {
                    holdLocation.localPosition = new Vector3(holdLocation.localPosition.x, holdLocation.localPosition.y, holdLocation.localPosition.z + (Input.mouseScrollDelta.y * 0.1f));
                }
            }
            else
            {
                if (!(holdLocation.localPosition.z < minRange))
                {
                    holdLocation.localPosition = new Vector3(holdLocation.localPosition.x, holdLocation.localPosition.y, holdLocation.localPosition.z + (Input.mouseScrollDelta.y * 0.1f));
                }
            }

            

        }

        //move prop
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(ray, out hit, 4.5f, layerMaskMoveableProp)){
                //case where player press input, selected prop stops being selected
                if (selectedProp == hit.transform.gameObject)
                {
                    DropSelectedProp();
                }
                else
                {
                    if (selectedProp != null)
                    {
                        DropSelectedProp();
                    }
                    //case where player press input, select hit prop, cache
                    selectedProp = hit.transform.gameObject;
                    selectedPropRb = hit.rigidbody;

                    selectedPropRb.useGravity = false;
                }
            }
            else
            {
                //case where player press input, no layerMaskMoveableProp rayhit, selected prop stops being selected
                if (selectedProp != null)
                {
                    DropSelectedProp();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(selectedProp != null)
            {
                if (Physics.Raycast(ray, out hit, 4.5f, layerMaskMoveableProp))
                {
                    selectedPropRb.AddForce(ray.direction*25, ForceMode.Impulse);
                    DropSelectedProp();
                }
            }
        }

        //end



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

    //temp for spell
    void FixedUpdate()
    {
        if (selectedProp != null)
        {

            Vector3 dir = holdLocation.position - selectedProp.transform.position;
            float dist = Vector3.Distance(holdLocation.position, selectedProp.transform.position);
            dir *= dist;
            selectedPropRb.AddForce(dir, ForceMode.VelocityChange);
            if (Input.GetKey(KeyCode.W))
            {
                selectedPropRb.velocity *= Mathf.Clamp(dist, 0.2f, 0.8f);
            }
            else
            {
                selectedPropRb.velocity *= Mathf.Clamp(dist, 0.2f, 0.6f);
            }
        }
    }

    //temp for spell
    private void DropSelectedProp()
    {
        selectedPropRb.useGravity = true;
        selectedProp = null;
        selectedPropRb = null;
        holdLocation.localPosition = new Vector3(0, 0, defaultRange);
    }
}
