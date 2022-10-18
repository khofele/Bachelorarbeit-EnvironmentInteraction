using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    private Collider objectCollider = null;
    public Collider ObjectCollider { get => objectCollider; }

    public override void Validate()
    {
        base.Validate();
        GetComponent<Rigidbody>().isKinematic = true;

        objectCollider = GetComponent<Collider>();
    }
}
