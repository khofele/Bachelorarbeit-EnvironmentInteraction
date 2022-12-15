using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastInteractionCondition : HighPriorityCondition
{
    [SerializeField] private Interaction precededInteraction = null;
    private bool isLastInteractionCorrect = false;


    public override bool CheckCondition()
    {
        return isLastInteractionCorrect;
    }

    private void Update()
    {
        if (interactionManager.LastInteraction != null)
        {
            if (interactionManager.LastInteraction.GetType() == precededInteraction.GetType())
            {
                isLastInteractionCorrect = true;
            }
            else
            {
                isLastInteractionCorrect = false;
            }
        }
        else
        {
            isLastInteractionCorrect = false;
        }
    }
}
