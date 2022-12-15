using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAlreadyRunningCondition : BaseCondition
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
