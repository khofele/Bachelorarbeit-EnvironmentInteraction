using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionManager))]
public abstract class Interaction : MonoBehaviour
{
    public ConditionManager conditionManager = null;
    public InteractionManager interactionManager = null;
    public AnimationManager animationManager = null;
    public InteractableManager interactableManager = null;
    public CharController charController = null;
    public IKController iKController = null;
    public bool isInteracting = false;
    public Type matchingInteractable = null;

    public bool IsInteracting { get => isInteracting; }

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

    public virtual void Start()
    {
        conditionManager = GetComponent<ConditionManager>();
        interactionManager = GetComponentInParent<InteractionManager>();
        animationManager = GetComponentInParent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
        charController = GetComponentInParent<CharController>();
        iKController = GetComponentInParent<IKController>();

        conditionManager.FillConditionsList();
    }

    public virtual void Update()
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

    public virtual bool CheckTrigger()
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

    public virtual bool CheckConditions()
    {
        return conditionManager.CheckConditions();
    }

    public virtual void ExecuteInteraction()
    {
        interactionManager.SetLastInteraction();
        // TODO evtl. anders setzen --> beim Lean wird das andauernd aufgerufen
        // evtl. nur wenn current != last?
    }

    public abstract void ResetInteraction();
}
