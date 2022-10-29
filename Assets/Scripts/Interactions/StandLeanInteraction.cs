using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandLeanInteraction : LeanInteraction
{
    protected override void ExecuteAnimation()
    {
        animationManager.ExecuteStandLeanAnimation(charController.XAxis);
    }

    protected override void StopAnimation()
    {
        animationManager.StopStandLeanAnimation();
    }

    protected override bool CheckTerminationCondition()
    {
        if(offset.magnitude > snapDistance || isInteractionRunning == false || (charController.ZAxis >= 1 || charController.ZAxis <= -1))
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
        interactionManager.IsStandingLeaning = false;
    }

    protected override void SetLeanBool(bool value)
    {
        interactionManager.IsStandingLeaning = value;
    }

    protected override bool CheckLeaningBool()
    {
        return interactionManager.IsStandingLeaning;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Leanable);
    }
}
