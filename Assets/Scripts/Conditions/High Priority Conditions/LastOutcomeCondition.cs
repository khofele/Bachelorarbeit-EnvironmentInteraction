using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastOutcomeCondition : HighPriorityCondition
{
    [SerializeField] private MultipleOutcomesInteraction multipleInteractionParent = null;
    [SerializeField] private Outcome precededOutcome = null;
    private bool isLastOutcomeCorrect = false;

    private void Update()
    {
        if (interactionManager.LastInteraction != null)
        {
            if (interactionManager.LastInteraction.GetType() == multipleInteractionParent.GetType())
            {
                MultipleOutcomesInteraction lastInteraction = (MultipleOutcomesInteraction)interactionManager.LastInteraction;

                if(lastInteraction.OutcomeManager.LastOutcome != null)
                {
                    if (lastInteraction.OutcomeManager.LastOutcome.GetType() == precededOutcome.GetType())
                    {
                        isLastOutcomeCorrect = true;
                    }
                    else
                    {
                        isLastOutcomeCorrect = false;
                    }
                }
            }
        }
        else
        {
            isLastOutcomeCorrect = false;
        }
    }

    public override bool CheckCondition()
    {
        return isLastOutcomeCorrect;
    }
}
