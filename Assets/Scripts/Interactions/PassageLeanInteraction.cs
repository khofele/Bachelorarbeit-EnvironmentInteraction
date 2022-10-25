using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageLeanInteraction : FixedLeanInteraction
{
    public override void SetLeanBool(bool value)
    {
        charController.IsLeaningInPassage = value;
    }

    public override bool CheckLeaningBool()
    {
        return charController.IsLeaningInPassage;
    }

    public override Type GetInteractableType()
    {
        return typeof(PassageLeanable);
    }

    public override void SetCurrentLeanable()
    {
        currentLeanable = (PassageLeanable)interactableManager.CurrentInteractable;
    } 
}
