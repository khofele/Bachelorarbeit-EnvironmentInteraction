using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAlreadyInteractingCondition : Condition
{
    public override bool CheckCondition()
    {
        if(GetComponent<FixedLeanInteraction>().IsCharInteracting == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
