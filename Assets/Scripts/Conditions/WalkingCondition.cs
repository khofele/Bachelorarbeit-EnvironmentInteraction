using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCondition : Condition
{
    public override bool CheckCondition()
    {
        return charController.IsWalking;
    }
}
