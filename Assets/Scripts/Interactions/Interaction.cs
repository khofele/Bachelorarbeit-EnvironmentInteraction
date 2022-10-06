using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionManager))]
public abstract class Interaction : MonoBehaviour
{
    private ConditionManager conditionManager = null;

    public abstract bool CheckTrigger();
    
    public abstract void ExecuteInteraction();

    private void Start()
    {
        conditionManager = GetComponent<ConditionManager>();
        conditionManager.FillConditionsList();
    }

    public virtual bool CheckConditions()
    {
        return conditionManager.CheckConditions();
    }
}
