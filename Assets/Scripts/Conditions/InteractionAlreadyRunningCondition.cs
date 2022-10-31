using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAlreadyRunningCondition : Condition
{
    // DEBUG
    // TODO falls behalten schön machen
    private InteractionManager interactionManager = null;

    protected override void Start()
    {
        base.Start();
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    public override bool CheckCondition()
    {
        if(interactionManager.CurrentInteraction != null)
        {
            return !interactionManager.CurrentInteraction.IsInteractionRunning;
        }
        else
        {
            return true;
        }
    }
}
