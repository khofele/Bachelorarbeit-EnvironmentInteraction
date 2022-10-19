using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Interaction lastInteraction = null;
    private Interaction currentInteraction = null;
    private bool isInteractionTriggered = false;

    public Interaction LastInteraction { get => lastInteraction; set => lastInteraction = value; }
    public Interaction CurrentInteraction { get => currentInteraction; set => currentInteraction = value; }
    public bool IsInteractionTriggered { get => isInteractionTriggered; set => isInteractionTriggered = value; }

    public void SetLastInteraction()
    {
        lastInteraction = currentInteraction;
    }
}
