using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectNearbyCondition : ObjectNearbyCondition
{
    protected override bool CheckMatchingComponent(Collider other)
    {
        if (other.gameObject.GetComponent<PushableTarget>() != null)
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
        target = other.gameObject.GetComponent<PushableTarget>();
    }
}
