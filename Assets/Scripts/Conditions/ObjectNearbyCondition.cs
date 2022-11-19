using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ObjectNearbyCondition : Condition
{
    private SphereCollider checkCollider = null;
    private bool isObjectNearby = false;
    private TargetObject target = null;

    public TargetObject Target { get => target; }

    protected override void Start()
    {
        base.Start();

        checkCollider = GetComponent<SphereCollider>();
        checkCollider.isTrigger = true;
    }

    public override bool CheckCondition()
    {
        return isObjectNearby;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("TargetObjects"))
        {
            if (other.gameObject.GetComponent<TargetObject>() != null)
            {
                target = other.gameObject.GetComponent<TargetObject>();
                isObjectNearby = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TargetObject>() != null && other.gameObject.layer == LayerMask.NameToLayer("TargetObjects"))
        {
            isObjectNearby = false;
        }
    }
}
