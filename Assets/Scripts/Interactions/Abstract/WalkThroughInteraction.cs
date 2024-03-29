using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkThroughInteraction : Interaction
{
    protected WalkThroughable currentInteractable = null;

    protected override void Update()
    {
        base.Update();

        if (isInteractionRunning == true && currentInteractable != null)
        {
            if (currentInteractable.IsTriggered == true)
            {
                finalIKController.IsIkActive = true;
            }
            else
            {
                finalIKController.IsIkActive = false;
                isInteractionRunning = false;
                isTriggeredByInterruptibleInteraction = false;
                ResetInteraction();
            }
        }
    }

    protected abstract void SetCurrentInteraction();

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        SetCurrentInteraction();
    }
}
