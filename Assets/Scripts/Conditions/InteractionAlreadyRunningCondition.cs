using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAlreadyRunningCondition : Condition
{
    public override bool CheckCondition()
    {
        if (interactionManager.CurrentInteraction != null)
        {
            return !interactionManager.CurrentInteraction.IsInteractionRunning;
        }
        else
        {
            return true;
        }
    }
}
