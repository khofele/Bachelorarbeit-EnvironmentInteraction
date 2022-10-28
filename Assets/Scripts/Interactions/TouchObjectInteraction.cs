using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObjectInteraction : Interaction
{
    private Touchable currentTouchable = null;

    public override void Start()
    {
        base.Start();
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        currentTouchable = (Touchable)interactableManager.CurrentInteractable;
    }

    public override void Update()
    {
        base.Update();

        if(isInteractionRunning == true && currentTouchable.IsTriggered == true)
        {
            iKController.IsIkActive = true;
        }
        else
        {
            iKController.IsIkActive = false;
        }
    }

    public override void ResetInteraction()
    {
        currentTouchable = null;
    }

    public override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Touchable);
    }
}
