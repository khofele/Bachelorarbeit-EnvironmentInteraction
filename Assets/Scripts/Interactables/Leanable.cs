using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    private Collider walkCollider = null;
    private Collider parentCollider = null;

    public Collider WalkCollider { get => walkCollider; }
    public Collider ParentCollider { get => parentCollider; }

    public override void Validate()
    {
        base.Validate();
        GetComponent<Rigidbody>().isKinematic = true;

        parentCollider = GetComponent<Collider>();

        if (transform.childCount == 0)
        {
            GameObject emptyGameObject = new GameObject();
            emptyGameObject.transform.SetParent(transform);

            emptyGameObject.AddComponent(ParentCollider.GetType());
            emptyGameObject.transform.localPosition = Vector3.zero;
        }
        else if(GetComponentInChildren<Collider>() == null)
        {
            transform.GetChild(0).gameObject.AddComponent(ParentCollider.GetType());
        }

        walkCollider = GetComponentInChildren<Collider>();
    }
}
