using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    private List<BaseCondition> baseConditions = new List<BaseCondition>();
    private List<BasicPriorityCondition> basicPriorityConditions = new List<BasicPriorityCondition>();
    private List<HighPriorityCondition> highPriorityConditions = new List<HighPriorityCondition>();

    public bool CheckBasicPriorityConditions()
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

    public bool CheckHighPriorityConditions()
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

    public bool CheckBaseConditions()
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

    public void FillConditionsLists()
    {
        basicPriorityConditions = GetComponents<BasicPriorityCondition>().ToList();
        highPriorityConditions = GetComponents<HighPriorityCondition>().ToList();
        baseConditions = GetComponents<BaseCondition>().ToList();
    }
}
