using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchLeanInteraction : LeanInteraction
{
    public override void Start()
    {
        base.Start();
    }
    public override void ExecuteAnimation()
    {
        animationManager.ExecuteCrouchLeanAnimation(charController.XAxis);
    }

    public override void StopAnimation()
    {
        animationManager.StopCrouchLeanAnimation();
    }

    public override bool CheckTerminationCondition()
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

    public override void ResetCharacter()
    {
        charController.IsCrouchingLeaning = false;
    }

    public override void SetLeanBool(bool value)
    {
        charController.IsCrouchingLeaning = value;
    }

    public override bool CheckLeaningBool()
    {
        return charController.IsCrouchingLeaning;
    }
}
