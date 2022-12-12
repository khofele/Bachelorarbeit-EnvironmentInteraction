using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    protected Collider snapCollider = null;

    public Collider SnapCollider { get => snapCollider; }

    protected override void Validate()
    {
        base.Validate();

        GetSnapCollider();
    }

    protected virtual void GetSnapCollider()
    {
        BoxCollider[] children = transform.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider child in children)
        {
            if (child.CompareTag("snapCollider"))
            {
                snapCollider = child;
                break;
            }
        }

        if (snapCollider == null)
        {
            Debug.LogError("The interactable object is missing a snap collider!");
        }
    }
}
