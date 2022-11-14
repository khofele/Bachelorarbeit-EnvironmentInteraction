using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    private Collider bodyCollider = null;

    public Collider BodyCollider { get => bodyCollider; }

    protected override void Validate()
    {
        base.Validate();

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider collider in colliders)
        {
            if(collider.isTrigger == false)
            {
                bodyCollider = collider;
            }
        }
    }
}
