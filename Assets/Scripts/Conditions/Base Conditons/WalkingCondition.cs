using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCondition : BaseCondition
{
    public override bool CheckCondition()
    {
        return charController.IsWalking;
    }
}
