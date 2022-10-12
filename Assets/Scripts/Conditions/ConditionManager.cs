using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    private List<Condition> conditions = new List<Condition>();

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

    public void FillConditionsList()
    {
        conditions = GetComponents<Condition>().ToList(); 
    }

    public void ResetConditions()
    {
        foreach (Condition condition in conditions)
        {
            condition.ResetCondition();
        }
    }
}
