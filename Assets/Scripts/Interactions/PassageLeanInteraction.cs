using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageLeanInteraction : FixedLeanInteraction
{
    public override void SetLeanBool(bool value)
    {
        interactionManager.IsLeaningInPassage = value;
    }

    public override bool CheckLeaningBool()
    {
        return interactionManager.IsLeaningInPassage;
    }

    public override void SetCurrentLeanable()
    {
        currentLeanable = (PassageLeanable)interactableManager.CurrentInteractable;
    }

    public override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(PassageLeanable);
    }
}
