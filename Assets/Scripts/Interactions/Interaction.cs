using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    private List<Condition> conditions = new List<Condition>();

    public abstract bool CheckTrigger();
    
    public abstract void ExecuteInteraction();
    
    public bool CheckConditions()
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].Check() == false)
            {
                return false;
            }
        }
        return true;
    }

    public void FillConditionsList()
    {
        // TODO implement
    }
}
