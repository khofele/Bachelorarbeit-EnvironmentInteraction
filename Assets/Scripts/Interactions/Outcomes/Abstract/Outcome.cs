using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Outcome : MonoBehaviour
{
    protected List<Condition> conditions = new List<Condition>();
    protected OutcomeManager outcomeManager = null;
    protected CharController charController = null;
    protected InteractionManager interactionManager = null;
    protected AnimationManager animationManager = null;
    protected FinalIKController iKController = null;

    public List<Condition> Conditions { get => conditions; }


    protected virtual void Start()
    {
        conditions = GetComponents<Condition>().ToList();

        outcomeManager = GetComponentInParent<OutcomeManager>();
        charController = FindObjectOfType<CharController>();
        interactionManager = FindObjectOfType<InteractionManager>();
        animationManager = FindObjectOfType<AnimationManager>();
        iKController = FindObjectOfType<FinalIKController>();
    }

    public bool CheckConditions()
    {
        foreach (Condition condition in conditions)
        {
            if (condition.CheckCondition() == false)
            {
                return false;
            }
        }
        return true;
    }

    public virtual void ExecuteOutcome()
    {
        outcomeManager.CurrentOutcome = this;
        iKController.IsIkActive = true;
    }

    public abstract void ResetOutcome();
}
