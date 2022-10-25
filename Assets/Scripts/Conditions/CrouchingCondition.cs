using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingCondition : Condition
{
    private bool isCrouching = false;

    private void Update()
    {
        if(charController.IsCrouching == true)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    public override bool CheckCondition()
    {
        return isCrouching; 
    }
}
