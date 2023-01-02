using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterruptibleInteraction : Interaction
{
    [SerializeField] protected List<Interaction> interruptInteractions = new List<Interaction>();

    protected bool CheckInterruptInteractionsConditions()
    {
        foreach (Interaction interaction in interruptInteractions)
        {
            if (interaction.CheckConditions() == true && interaction.IsInteractionTriggered == true)
            {
                interaction.IsTriggeredByInterrupibleInteraction = true;
                return true;
            }
        }

        return false;
    }
}
