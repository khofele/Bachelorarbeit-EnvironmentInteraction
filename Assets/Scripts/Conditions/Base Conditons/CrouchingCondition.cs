using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingCondition : BaseCondition
{
    public override bool CheckCondition()
    {
        return charController.IsCrouching; 
    }
}
