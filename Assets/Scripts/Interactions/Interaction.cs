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
    public bool isInteracting = false;

    public bool IsInteracting { get => isInteracting; }

    public virtual void Start()
    {
        conditionManager = GetComponent<ConditionManager>();
        interactionManager = GetComponentInParent<InteractionManager>();
        animationManager = GetComponentInParent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();

        conditionManager.FillConditionsList();
    }

    public virtual void Update()
    {
        if (CheckTrigger() == true)
        {
            if (CheckConditions() == true)
            {
                ExecuteInteraction();
                ResetInteraction();
                conditionManager.ResetConditions();
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
        interactionManager.IsInteractionTriggered = false;
    }

    public abstract void ResetInteraction();
}
