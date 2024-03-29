using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Interaction lastInteraction = null;
    private Interaction currentInteraction = null;
    private List<Interaction> allInteractions = new List<Interaction>();
    private InteractableManager interactableManager = null;
    private bool isLeaningSnapping = false;

    // Interaction bools
    private bool isLeaning = false;
    private bool isSnapping = false;

    private bool isFixedLeaning = false;
    private bool isLeaningOnEdge = false;
    private bool isCrouchingLeaning = false;
    private bool isStandingLeaning = false;
    private bool isLeaningInPassage = false;
    private bool isJumping = false;
    private bool isFixedSnapping = false;
    private bool isFighting = false;
    private bool isCharSnappingToEnemy = false;
    private bool isClimbing = false;
    private bool isClimbingSnapping = false;
    private bool isPushingObject = false;


    public bool IsLeaning { get => isLeaning; }
    public bool IsSnapping { get => isSnapping; }

    public bool IsFixedLeaning { get => isFixedLeaning; }
    public bool IsLeaningOnEdge { get => isLeaningOnEdge; set => isLeaningOnEdge = value; }
    public bool IsCrouchingLeaning { get => isCrouchingLeaning; set => isCrouchingLeaning = value; }
    public bool IsStandingLeaning { get => isStandingLeaning; set => isStandingLeaning = value; }
    public bool IsLeaningInPassage { get => isLeaningInPassage; set => isLeaningInPassage = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsFixedSnapping { get => isFixedSnapping; set => isFixedSnapping = value; }
    public bool IsFighting { get => isFighting; set => isFighting = value; }
    public bool IsCharSnappingToEnemy { get => isCharSnappingToEnemy; set => isCharSnappingToEnemy = value; }
    public bool IsClimbing { get => isClimbing; set => isClimbing = value; }
    public bool IsClimbingSnapping { get => isClimbingSnapping; set => isClimbingSnapping = value; }
    public bool IsPushingObject { get => isPushingObject; set => isPushingObject = value; }

    public Interaction LastInteraction { get => lastInteraction; set => lastInteraction = value; }
    public Interaction CurrentInteraction { get => currentInteraction; set => currentInteraction = value; }
    public bool IsLeaningSnapping { get => isLeaningSnapping; set => isLeaningSnapping = value; }

    private void Start()
    {
        interactableManager = FindObjectOfType<InteractableManager>();

        FillInteractionList();
    }

    private void Update()
    {
        if (isLeaningOnEdge == false && isCrouchingLeaning == false && isStandingLeaning == false && isLeaningInPassage == false)
        {
            isLeaning = false;
        }
        else
        {
            isLeaning = true;
        }

        if (isLeaningOnEdge == true || isLeaningInPassage == true)
        {
            isFixedLeaning = true;
        }
        else
        {
            isFixedLeaning = false;
        }

        if(isFixedSnapping == false && isLeaningSnapping == false && isCharSnappingToEnemy == false && isClimbingSnapping == false)
        {
            isSnapping = false;
        }
        else
        {
            isSnapping = true;
        }
    }

    private void FillInteractionList()
    {
        Interaction[] children = GetComponentsInChildren<Interaction>();
        allInteractions = children.ToList<Interaction>();
    }

    private Interaction GetCurrentInteractionFromTriggeredInteractable()
    {
        foreach (Interaction interaction in allInteractions)
        {
            if (interactableManager.CurrentInteractable.GetType() == interaction.MatchingInteractable)
            {
                return interaction;
            }
        }

        return null;
    }

    public Interaction GetCurrentInteraction(Interactable interactable)
    {
        foreach (Interaction interaction in allInteractions)
        {
            if (interactable.GetType() == interaction.MatchingInteractable)
            {
                return interaction;
            }
        }

        return null;
    }

    public bool CheckOtherInteractionsRunning()
    {
        if (interactableManager.CurrentInteractableParent != null)
        {
            foreach (Interaction interaction in allInteractions)
            {
                foreach (Interactable interactable in interactableManager.CurrentInteractableParent.Interactables)
                {
                    if (GetCurrentInteraction(interactable) != interaction && interaction.IsInteractionRunning == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        else
        {
            foreach (Interaction interaction in allInteractions)
            {
                if (GetCurrentInteractionFromTriggeredInteractable() != interaction && interaction.IsInteractionRunning == true)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public void SetLastInteraction()
    {
        if (lastInteraction != currentInteraction)
        {
            lastInteraction = currentInteraction;
        }
    }

    public bool CheckAllInteractionsRunning()
    {
        foreach (Interaction interaction in allInteractions)
        {
            if (interaction.IsInteractionRunning == true)
            {
                return true;
            }
        }

        return false;
    }
}
