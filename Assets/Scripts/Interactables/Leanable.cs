using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    protected Collider snapCollider = null;
    //protected Collider objectCollider = null;
    public Collider SnapCollider { get => snapCollider; }

    protected override void Validate()
    {
        base.Validate();

        //objectCollider = GetComponent<Collider>();
        GetComponent<Rigidbody>().isKinematic = true;

        //ModifyTrigger();
        GetSnapCollider();
    }

    //protected virtual void ModifyTrigger()
    //{
    //    if (objectCollider.GetType() == typeof(BoxCollider))
    //    {
    //        BoxCollider boxCollider = (BoxCollider)objectCollider;
    //    }
    //    else if (objectCollider.GetType() == typeof(SphereCollider))
    //    {
    //        SphereCollider sphereCollider = (SphereCollider)objectCollider;
    //    }
    //    else if (objectCollider.GetType() == typeof(CapsuleCollider))
    //    {
    //        CapsuleCollider capsuleCollider = (CapsuleCollider)objectCollider;
    //    }
    //}

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
