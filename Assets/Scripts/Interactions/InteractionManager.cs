using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Interaction lastInteraction = null;
    private Interaction currentInteraction = null;
    private bool isInteractionTriggered = false;

    // Interaction bools
    private bool isLeaningOnEdge = false;
    private bool isCrouchingLeaning = false;
    private bool isStandingLeaning = false;
    private bool isLeaningInPassage = false;

    public bool IsLeaningOnEdge { get => isLeaningOnEdge; set => isLeaningOnEdge = value; }
    public bool IsCrouchingLeaning { get => isCrouchingLeaning; set => isCrouchingLeaning = value; }
    public bool IsStandingLeaning { get => isStandingLeaning; set => isStandingLeaning = value; }
    public bool IsLeaningInPassage { get => isLeaningInPassage; set => isLeaningInPassage = value; }

    public Interaction LastInteraction { get => lastInteraction; set => lastInteraction = value; }
    public Interaction CurrentInteraction { get => currentInteraction; set => currentInteraction = value; }
    public bool IsInteractionTriggered { get => isInteractionTriggered; set => isInteractionTriggered = value; }

    public void SetLastInteraction()
    {
        lastInteraction = currentInteraction;
    }
}
