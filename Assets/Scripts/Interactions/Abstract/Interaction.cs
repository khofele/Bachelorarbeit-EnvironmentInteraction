using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionManager))]
public abstract class Interaction : MonoBehaviour
{
    protected ConditionManager conditionManager = null;
    protected InteractionManager interactionManager = null;
    protected AnimationManager animationManager = null;
    protected InteractableManager interactableManager = null;
    protected CharController charController = null;
    protected FinalIKController finalIKController = null;
    protected bool isInteractionRunning = false;
    protected bool isCharInteracting = false;
    protected Type matchingInteractable = null;
    protected bool isInteractionTriggered = false;
    protected bool isTriggeredByInterruptibleInteraction = false;



    public bool IsInteractionRunning { get => isInteractionRunning; set => isInteractionRunning = value; }
    public bool IsCharInteracting { get => isCharInteracting; }
    public Type MatchingInteractable { get => matchingInteractable; }
    public bool IsInteractionTriggered { get => isInteractionTriggered; set => isInteractionTriggered = value; }
    public bool IsTriggeredByInterrupibleInteraction { get => isTriggeredByInterruptibleInteraction; set => isTriggeredByInterruptibleInteraction = value; }

    protected bool CheckMatchingInteractable()
    {
        if (interactableManager.CurrentInteractableParent != null)
        {
            foreach (Interactable interactable in interactableManager.CurrentInteractableParent.Interactables)
            {
                if (interactable.GetType() == matchingInteractable)
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            if (interactableManager.CurrentInteractable != null)
            {
                if (interactableManager.CurrentInteractable.GetType() == matchingInteractable)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    protected virtual void Start()
    {
        conditionManager = GetComponent<ConditionManager>();
        interactionManager = GetComponentInParent<InteractionManager>();
        animationManager = GetComponentInParent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
        charController = GetComponentInParent<CharController>();
        finalIKController = GetComponentInParent<FinalIKController>();

        conditionManager.FillConditionsLists();
        SetMatchingInteractable();
    }

    protected virtual void Update()
    {
        if (CheckTrigger() == true)
        {
            if (CheckMatchingInteractable() == true)
            {
                if (CheckOtherInteractionsRunning() == true)
                {
                    if (CheckConditions() == true)
                    {
                        ExecuteInteraction();
                    }
                    else if(isTriggeredByInterruptibleInteraction == true)
                    {
                        ExecuteInteraction();
                    }
                }
            }
        }   
    }

    protected virtual bool CheckTrigger()
    {
        return isInteractionTriggered;
    }

    public virtual bool CheckConditions()
    {
        if (CheckBaseConditions() == true)
        {
            if (CheckHighPriorityConditions() == true)
            {
                return true;
            }
            else if (CheckBasicPriorityConditions() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected virtual bool CheckHighPriorityConditions()
    {
        return conditionManager.CheckHighPriorityConditions();
    }

    protected virtual bool CheckBasicPriorityConditions()
    {
        return conditionManager.CheckBasicPriorityConditions();
    }

    protected virtual bool CheckBaseConditions()
    {
        return conditionManager.CheckBaseConditions();
    }

    public virtual void ExecuteInteraction()
    {
        finalIKController.IsIkActive = true;
        interactionManager.CurrentInteraction = this;
        isInteractionRunning = true;

        SetCurrentInteractable();
    }

    protected virtual bool CheckOtherInteractionsRunning()
    {
        return interactionManager.CheckOtherInteractionsRunning();
    }

    protected virtual void ResetInteraction()
    {
        isInteractionRunning = false;
        isInteractionTriggered = false;
        finalIKController.IsIkActive = false;
        isTriggeredByInterruptibleInteraction = false;
        interactionManager.SetLastInteraction();
    }

    protected void SetCurrentInteractable()
    {
        foreach(Interactable interactable in interactableManager.CurrentInteractableParent.Interactables)
        {
            if(interactable.GetType() == matchingInteractable)
            {
                interactableManager.CurrentInteractable = interactable;
            }
        }
    }

    protected abstract void SetMatchingInteractable();
}
