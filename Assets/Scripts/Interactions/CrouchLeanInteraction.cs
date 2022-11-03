using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchLeanInteraction : LeanInteraction
{
    protected override void ExecuteAnimation()
    {
        animationManager.ExecuteCrouchLeanAnimation(charController.XAxis);
    }

    protected override void StopAnimation()
    {
        animationManager.StopCrouchLeanAnimation();
    }

    protected override bool CheckTerminationCondition()
    {
        if (offset.magnitude > snapDistance || charController.IsCrouching == false || isInteractionRunning == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void ResetCharacter()
    {
        interactionManager.IsCrouchingLeaning = false;
    }

    protected override void SetLeanBool(bool value)
    {
        interactionManager.IsCrouchingLeaning = value;
    }

    protected override bool CheckLeaningBool()
    {
        return interactionManager.IsCrouchingLeaning;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Leanable);
    }
}
