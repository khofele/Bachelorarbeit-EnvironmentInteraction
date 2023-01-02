using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Outcome : MonoBehaviour
{
    protected List<BasicPriorityCondition> basicPriorityConditions = new List<BasicPriorityCondition>();
    protected List<HighPriorityCondition> highPriorityConditions = new List<HighPriorityCondition>();
    protected List<BaseCondition> baseConditions = new List<BaseCondition>();
    protected OutcomeManager outcomeManager = null;
    protected CharController charController = null;
    protected InteractionManager interactionManager = null;
    protected AnimationManager animationManager = null;
    protected FinalIKController iKController = null;
    protected InteractableManager interactableManager = null;

    public List<BasicPriorityCondition> Conditions { get => basicPriorityConditions; }

    private bool CheckHighPriorityConditions()
    {
        if (highPriorityConditions.Count > 0)
        {
            foreach (HighPriorityCondition condition in highPriorityConditions)
            {
                if (condition.CheckCondition() == false)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckBasicPriorityConditions()
    {
        foreach (BasicPriorityCondition condition in basicPriorityConditions)
        {
            if (condition.CheckCondition() == false)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckBaseConditions()
    {
        foreach (BaseCondition baseCondition in baseConditions)
        {
            if (baseCondition.CheckCondition() == false)
            {
                return false;
            }
        }
        return true;
    }

    protected virtual void Start()
    {
        basicPriorityConditions = GetComponents<BasicPriorityCondition>().ToList();
        highPriorityConditions = GetComponents<HighPriorityCondition>().ToList();
        baseConditions = GetComponents<BaseCondition>().ToList();

        outcomeManager = GetComponentInParent<OutcomeManager>();
        charController = FindObjectOfType<CharController>();
        interactionManager = FindObjectOfType<InteractionManager>();
        animationManager = FindObjectOfType<AnimationManager>();
        iKController = FindObjectOfType<FinalIKController>();
        interactableManager = FindObjectOfType<InteractableManager>();
    }

    public bool CheckConditions()
    {
        if(CheckBaseConditions() == true)
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

    public virtual void ExecuteOutcome()
    {
        outcomeManager.CurrentOutcome = this;
        iKController.IsIkActive = true;
    }

    public virtual void ResetOutcome()
    {
        iKController.IsIkActive = false;
        outcomeManager.SetLastOutcome();
        outcomeManager.CurrentOutcome = null;
    }
}
