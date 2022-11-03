using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAlreadyInteractingCondition : Condition
{
    public override bool CheckCondition()
    {
        if (GetComponent<Interaction>().IsCharInteracting == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
