using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTargetNearbyCondition : ObjectNearbyCondition
{
    protected override bool CheckMatchingComponent(Collider other)
    {
        if (other.gameObject.GetComponent<FightTarget>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void SetTarget(Collider other)
    { 
        target = other.gameObject.GetComponent<FightTarget>();
    }
}
