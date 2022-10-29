using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLeanInteraction : FixedLeanInteraction
{
    protected override void SetLeanBool(bool value)
    {
        interactionManager.IsLeaningOnEdge = value;
    }

    protected override bool CheckLeaningBool()
    {
        return interactionManager.IsLeaningOnEdge;
    }

    protected override void SetCurrentLeanable()
    {
        currentLeanable = (EdgeLeanable)interactableManager.CurrentInteractable;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(EdgeLeanable);
    }
}
