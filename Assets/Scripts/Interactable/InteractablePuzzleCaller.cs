using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzleCaller : InteractablePuzzle
{
    //targets needs to contain a script that references to InteractableReceiver, so it can ref Interactable and get called with GetComp<Interactable>
    public GameObject[] receiversObj;

    //cache
    private InteractablePuzzleReceiver[] receivers;

    private void Start()
    {
        //cache comps
        receivers = new InteractablePuzzleReceiver[receiversObj.Length];
        for (int i = 0; i < receiversObj.Length; i++)
        {
            receivers[i] = receiversObj[i].GetComponent<InteractablePuzzleReceiver>();
        }
    }

    //player press caller, tell receivers
    public override void OnPress(int num)
    {
        for (int i = 0; i < receiversObj.Length; i++)
        {
            if (receivers[i].unlockOnPressFromCaller)
            {
                receivers[i].OnKeyUnlock();
            }

            if (receivers[i].PressOnPressFromCaller)
            {
                receivers[i].OnPressFromSwitch(1);
            }

            if (receivers[i].destroyOnPressFromCaller)
            {
                receiversObj[i].SetActive(false);
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
