using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCondition : Condition
{
    [SerializeField] private int probabilityCases = 1;
    private bool result = false;
    private bool isExecuted = false;

    // DEBUG
    public bool IsExecuted { get => isExecuted; set => isExecuted = value; }

    public override bool CheckCondition()
    {
        return CalculateResult();
    }

    private bool CalculateResult()
    {
        if (isExecuted == false)
        {
            isExecuted = true;

            int random = Random.Range(1, 10);

            if (random % probabilityCases == 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        return result;
    }
}
