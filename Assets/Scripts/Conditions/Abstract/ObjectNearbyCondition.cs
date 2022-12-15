using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class ObjectNearbyCondition : BaseCondition
{
    private SphereCollider checkCollider = null;
    private bool isObjectNearby = false;
    protected TargetObject target = null;

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
        if (other.gameObject.layer == LayerMask.NameToLayer("TargetObjects"))
        {
            if (CheckMatchingComponent(other) == true)
            {
                SetTarget(other);
                isObjectNearby = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckMatchingComponent(other) == true && other.gameObject.layer == LayerMask.NameToLayer("TargetObjects"))
        {
            isObjectNearby = false;
        }
    }

    protected abstract bool CheckMatchingComponent(Collider other);
    protected abstract void SetTarget(Collider other);
}
