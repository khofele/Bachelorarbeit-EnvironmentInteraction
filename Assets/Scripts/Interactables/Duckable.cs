using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duckable : WalkThroughable
{
    protected override void SetCurrentInteraction()
    {
        currentInteraction = (DuckingInteraction)interactionManager.CurrentInteraction;
    }
}
