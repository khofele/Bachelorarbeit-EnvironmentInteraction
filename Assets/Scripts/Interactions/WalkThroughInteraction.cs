using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkThroughInteraction : Interaction
{
    protected WalkThroughable currentInteractable = null;

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        SetCurrentInteraction();
    }

    protected override void Update()
    {
        base.Update();

        if (isInteractionRunning == true && currentInteractable != null)
        {
            if (currentInteractable.IsTriggered == true)
            {
                iKController.IsIkActive = true;
            }
            else
            {
                iKController.IsIkActive = false;
                isInteractionRunning = false;
                ResetValues();
            }
        }
        else
        {
            iKController.IsIkActive = false;
            isInteractionRunning = false;
        }
    }

    protected abstract void SetCurrentInteraction();
    protected abstract void ResetValues();
}
