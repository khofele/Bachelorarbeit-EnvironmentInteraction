using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    public Collider snapCollider = null;
    public Collider objectCollider = null;
    public Collider SnapCollider { get => snapCollider; }

    public override void Validate()
    {
        base.Validate();

        GetComponent<Rigidbody>().isKinematic = true;
        objectCollider = GetComponent<Collider>();

        AddSnapCollider();
        ModifyTrigger();
    }

    public virtual void AddSnapCollider()
    {
        if (transform.childCount == 0)
        {
            GameObject emptyGameObject = new GameObject("Collider");
            emptyGameObject.transform.SetParent(transform);

            emptyGameObject.AddComponent(objectCollider.GetType());
            emptyGameObject.transform.localPosition = Vector3.zero;
            snapCollider = emptyGameObject.GetComponent<Collider>();

        }
        else if (GetComponentInChildren<Collider>() == null)
        {
            transform.GetChild(0).gameObject.AddComponent(objectCollider.GetType());
            snapCollider = transform.GetChild(0).GetComponent<Collider>();
        }
        else
        {
            snapCollider = transform.GetChild(0).GetComponent<Collider>();
        }
    }

    public virtual void ModifyTrigger()
    {
        // TODO für alle Colliderformen bauen
        if (objectCollider.GetType() == typeof(BoxCollider))
        {
            BoxCollider boxCollider = (BoxCollider)objectCollider;
            boxCollider.size = new Vector3(1.2f, 1f, 2f);
        }
    }
}
