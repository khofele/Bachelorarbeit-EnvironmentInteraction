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
    private bool isLeaning = false;
    private bool isFixedLeaning = false;
    private bool isLeaningOnEdge = false;
    private bool isCrouchingLeaning = false;
    private bool isStandingLeaning = false;
    private bool isLeaningInPassage = false;
    private bool isJumping = false;

    public bool IsLeaning { get => isLeaning; }
    public bool IsFixedLeaning { get => isFixedLeaning; }
    public bool IsLeaningOnEdge { get => isLeaningOnEdge; set => isLeaningOnEdge = value; }
    public bool IsCrouchingLeaning { get => isCrouchingLeaning; set => isCrouchingLeaning = value; }
    public bool IsStandingLeaning { get => isStandingLeaning; set => isStandingLeaning = value; }
    public bool IsLeaningInPassage { get => isLeaningInPassage; set => isLeaningInPassage = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }

    public Interaction LastInteraction { get => lastInteraction; set => lastInteraction = value; }
    public Interaction CurrentInteraction { get => currentInteraction; set => currentInteraction = value; }
    public bool IsInteractionTriggered { get => isInteractionTriggered; set => isInteractionTriggered = value; }


    private void Update()
    {
        if(isLeaningOnEdge == false && isCrouchingLeaning == false && isStandingLeaning == false && isLeaningInPassage == false)
        {
            isLeaning = false;
        }
        else
        {
            isLeaning = true;
        }

        if(isLeaningOnEdge == true || isLeaningInPassage == true)
        {
            isFixedLeaning = true;
        }
        else
        {
            isFixedLeaning = false;
        }
    }

    public void SetLastInteraction()
    {
        lastInteraction = currentInteraction;
    }
}
