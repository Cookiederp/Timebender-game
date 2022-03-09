using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skeleton class, scripts such as Door, button, lever... should ref this
public class InteractablePuzzle : Interactable
{
    //targets needs to contain a script that references to InteractableReceiver, so it can ref Interactable and get called with GetComp<Interactable>
    [SerializeField]
    public GameObject[] targets;

    //cache
    private Interactable[] targetsInteractComp;

    [SerializeField]
    private bool[] onPressDestroyTargetsBool;

    [SerializeField]
    private bool[] onPressPressTargetsBool;

    private void Start()
    {
        //cache comps
        targetsInteractComp = new Interactable[targets.Length];
        for(int i = 0; i<targets.Length; i++)
        {
            targetsInteractComp[i] = targets[i].GetComponent<Interactable>();
        }
    }

    public override void OnPress()
    {
        for (int i = 0; i < onPressPressTargetsBool.Length; i++)
        {
            if (onPressPressTargetsBool[i])
            {
                if (targets[i] != null)
                {
                    targetsInteractComp[i].OnPress();
                    Debug.Log("Pressed: " + targets[i].name);
                }
            }
        }

        for (int i = 0; i < onPressDestroyTargetsBool.Length; i++)
        {
            if (onPressDestroyTargetsBool[i])
            {
                if(targets[i] != null)
                {
                    Destroy(targets[i]);
                }
            }
        }
    }
}

