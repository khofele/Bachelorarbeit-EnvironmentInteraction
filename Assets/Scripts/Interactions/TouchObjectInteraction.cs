using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObjectInteraction : Interaction
{
    private Touchable currentTouchable = null;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        currentTouchable = (Touchable)interactableManager.CurrentInteractable;
    }

    protected override void Update()
    {
        base.Update();

        if(isInteractionRunning == true && currentTouchable != null)
        {
            if(currentTouchable.IsTriggered == true)
            {
                iKController.IsIkActive = true;
            }
        }
        else
        {
            iKController.IsIkActive = false;
        }
    }

    protected override void ResetInteraction()
    {
        //currentTouchable = null;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Touchable);
    }
}
