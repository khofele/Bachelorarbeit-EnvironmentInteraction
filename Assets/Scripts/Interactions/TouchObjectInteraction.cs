using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObjectInteraction : WalkThroughInteraction
{
    private bool isInteractionOver = false;

    public bool IsInteractionOver { get => isInteractionOver; set => isInteractionOver = value; }


    protected override void ResetInteraction()
    {
        // TODO 03.11. Reset verzögern? --> siehe JumpInteraction --> siehe TODO in Interaction
    }

    protected override void ResetValues()
    {
        currentInteractable = null;
    }

    protected override void SetCurrentInteraction()
    {
        currentInteractable = (Touchable)interactableManager.CurrentInteractable;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Touchable);
    }
}
