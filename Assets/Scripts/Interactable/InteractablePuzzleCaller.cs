using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzleCaller : InteractablePuzzle
{
    //targets needs to contain a script that references to InteractableReceiver, so it can ref Interactable and get called with GetComp<Interactable>
    [SerializeField]
    public GameObject[] targets;

    //cache
    private InteractablePuzzleReceiver[] targetsInteractComp;

    [SerializeField]
    private bool[] onPressDestroyTargetsBool;

    [SerializeField]
    private bool[] onPressPressTargetsBool;

    private void Start()
    {
        //cache comps
        targetsInteractComp = new InteractablePuzzleReceiver[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            targetsInteractComp[i] = targets[i].GetComponent<InteractablePuzzleReceiver>();
        }
    }

    public override void OnPress(int num)
    {
        for (int i = 0; i < onPressPressTargetsBool.Length; i++)
        {
            if (onPressPressTargetsBool[i])
            {
                if (targets[i] != null)
                {
                    targetsInteractComp[i].OnPressFromSwitch(1);
                    Debug.Log("Pressed: " + targets[i].name);
                }
            }
        }

        for (int i = 0; i < onPressDestroyTargetsBool.Length; i++)
        {
            if (onPressDestroyTargetsBool[i])
            {
                if (targets[i] != null)
                {
                    Destroy(targets[i]);
                }
            }
        }
    }


    public override void ShowMessage(int index, string message)
    {
        base.ShowMessage(index, message);
    }

    public override void ShowMessageExit()
    {
        base.ShowMessageExit();
    }
}
