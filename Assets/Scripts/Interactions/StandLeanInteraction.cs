using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandLeanInteraction : LeanInteraction
{
    public override void ExecuteAnimation()
    {
        animationManager.ExecuteStandLeanAnimation(charController.XAxis);
    }

    public override void StopAnimation()
    {
        animationManager.StopStandLeanAnimation();
    }

    public override bool CheckTerminationCondition()
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

    public override void ResetCharacter()
    {
        charController.IsStandingLeaning = false;
    }

    public override void SetLeanBool(bool value)
    {
        charController.IsStandingLeaning = value;
    }

    public override bool CheckLeaningBool()
    {
        return charController.IsStandingLeaning;
    }
}
