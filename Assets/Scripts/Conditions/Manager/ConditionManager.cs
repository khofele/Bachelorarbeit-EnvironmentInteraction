using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    private List<Condition> baseConditions = new List<Condition>();

    public bool CheckConditions()
    {
        foreach (Condition condition in baseConditions)
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
        baseConditions = GetComponents<Condition>().ToList();
    }
}
