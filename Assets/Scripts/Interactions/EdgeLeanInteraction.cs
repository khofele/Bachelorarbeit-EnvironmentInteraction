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

    public override Type GetInteractableType()
    {
        return typeof(EdgeLeanable);
    }

    public override void SetCurrentLeanable()
    {
        currentLeanable = (EdgeLeanable)interactableManager.CurrentInteractable;
    }
}
