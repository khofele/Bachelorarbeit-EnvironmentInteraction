using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLeanInteraction : FixedLeanInteraction
{
    public override void SetLeanBool(bool value)
    {
        interactionManager.IsLeaningOnEdge = value;
    }

    public override bool CheckLeaningBool()
    {
        return interactionManager.IsLeaningOnEdge;
    }

    public override void SetCurrentLeanable()
    {
        currentLeanable = (EdgeLeanable)interactableManager.CurrentInteractable;
    }

    public override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(EdgeLeanable);
    }
}
