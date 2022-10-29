using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageLeanInteraction : FixedLeanInteraction
{
    protected override void SetLeanBool(bool value)
    {
        interactionManager.IsLeaningInPassage = value;
    }

    protected override bool CheckLeaningBool()
    {
        return interactionManager.IsLeaningInPassage;
    }

    protected override void SetCurrentLeanable()
    {
        currentLeanable = (PassageLeanable)interactableManager.CurrentInteractable;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(PassageLeanable);
    }
}
