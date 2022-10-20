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

    public override bool TerminationCondition()
    {
        // TODO noch mehr Bedingungen?
        if(offset.magnitude > snapDistance || isInteracting == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
