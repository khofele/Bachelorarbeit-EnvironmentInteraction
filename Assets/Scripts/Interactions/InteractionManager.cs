using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Interaction lastInteraction;
    private Interaction currentInteraction;
    private bool isTriggered = false;
    
    public Interaction LastInteraction { get => lastInteraction; set => lastInteraction = value; }
    public Interaction CurrentInteraction { get => currentInteraction; set => currentInteraction = value; }
    public bool IsTriggered { get => isTriggered; set => isTriggered = value; }

    public void SetLastInteraction()
    {
        lastInteraction = currentInteraction;
    }
}
