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

    public override bool TerminationCondition()
    {
        if (offset.magnitude > snapDistance || charController.IsCrouching == false || isInteracting == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
