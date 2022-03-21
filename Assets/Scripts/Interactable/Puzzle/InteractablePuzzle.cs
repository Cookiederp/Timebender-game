using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skeleton class, scripts such as Door, button, lever... should ref this
public class InteractablePuzzle : Interactable
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }
    
    public virtual void ShowMessage(int index, string message)
    {
        gameManager.uiInteractManager.UpdateHighlightInfoText(index, message);
    }

    public virtual void ShowMessageExit()
    {
        gameManager.uiInteractManager.UpdateHighlightInfoText(-1);
    }
    
}

