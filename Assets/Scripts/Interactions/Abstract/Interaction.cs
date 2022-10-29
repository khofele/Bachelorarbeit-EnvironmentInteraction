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
    protected IKController iKController = null;
    protected bool isInteractionRunning = false;
    protected Type matchingInteractable = null;

    public bool IsInteractionRunning { get => isInteractionRunning; set => isInteractionRunning = value; }

    private bool CheckMatchingInteractable()
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

    protected virtual void Start()
    {
        conditionManager = GetComponent<ConditionManager>();
        interactionManager = GetComponentInParent<InteractionManager>();
        animationManager = GetComponentInParent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
        charController = GetComponentInParent<CharController>();
        iKController = GetComponentInParent<IKController>();

        conditionManager.FillConditionsList();
        SetMatchingInteractable();
    }

    protected virtual void Update()
    {
        if (CheckTrigger() == true)
        {
            if(CheckMatchingInteractable() == true)
            {
                if (CheckConditions() == true)
                {
                    ExecuteInteraction();
                    ResetInteraction();
                }
            }
        }
    }

    protected virtual bool CheckTrigger()
    {
        if (interactionManager.IsInteractionTriggered == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual bool CheckConditions()
    {
        return conditionManager.CheckConditions();
    }

    protected virtual void ExecuteInteraction()
    {
        iKController.IsIkActive = true;
        interactionManager.CurrentInteraction = this;
        isInteractionRunning = true;
        interactionManager.SetLastInteraction();
        // TODO evtl. anders setzen --> beim Lean wird das andauernd aufgerufen
        // evtl. nur wenn current != last?
    }

    protected abstract void ResetInteraction();
    protected abstract void SetMatchingInteractable();
}
