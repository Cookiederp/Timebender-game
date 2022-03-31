using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTimeTravelProps : MonoBehaviour
{
    private Camera camera;
    private float interactRange = 3.5f;
    int layerMaskMoveable;
    int layerMaskInteractableMoveable = 1 << 9;

    private GameManager gameManager;
    private PlayerInteractManager playerInteractManager;

    // Start is called before the first frame update
    void Awake()
    {
        layerMaskMoveable = LayerMask.NameToLayer("InteractableMoveable");
        camera = Camera.main;
        gameManager = GameManager.instance;
        playerInteractManager = FindObjectOfType<PlayerInteractManager>();
    }


    private void OnEnable()
    {
        gameManager.isSpellTimeTravelActive = true;
        playerInteractManager.UpdateTimeText();
    }

    private void OnDisable()
    {
        gameManager.isSpellTimeTravelActive = false;
        gameManager.uiInteractManager.UpdateTimeSelectionText(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePaused)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            //right, to future
            if (Input.GetMouseButtonDown(1))
            {
                //another raycast when key is hit to make sure target taken is not behind anything
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    Transform objectHit = hit.transform;
                    if (layerMaskMoveable == objectHit.gameObject.layer)
                    {
                        //only objects that player can put in present or future
                        Interactable obj = objectHit.gameObject.GetComponent<TimeTravelReceiver>();
                        obj.OnPress(1);
                    }
                }
            }

            //left, to present
            if (Input.GetMouseButtonDown(0))
            {
                //another raycast when key is hit to make sure target taken is not behind anything
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    Transform objectHit = hit.transform;
                    if (layerMaskMoveable == objectHit.gameObject.layer)
                    {
                        //only objects that player can put in present or future
                        Interactable obj = objectHit.gameObject.GetComponent<TimeTravelReceiver>();
                        obj.OnPress(-1);
                    }
                }
            }
        }

    }
}
