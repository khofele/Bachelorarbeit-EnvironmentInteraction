using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    protected Collider snapCollider = null;
    protected Collider objectCollider = null;
    public Collider SnapCollider { get => snapCollider; }

    protected override void Validate()
    {
        base.Validate();

        GetComponent<Rigidbody>().isKinematic = true;
        objectCollider = GetComponent<Collider>();

        AddSnapCollider();
        ModifyTrigger();
    }


    // TODO schön machen + ausbessern  --> Validate-Ticket
    protected virtual void AddSnapCollider()
    {
        //if(GetComponentInChildren<Collider>() == null)
        //{
            GameObject emptyGameObject = new GameObject("Collider");
            emptyGameObject.transform.SetParent(transform);

            emptyGameObject.AddComponent(objectCollider.GetType());
            emptyGameObject.transform.localPosition = Vector3.zero;
            emptyGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            snapCollider = emptyGameObject.GetComponent<Collider>();

        //}
        //else if (GetComponentInChildren<Collider>() != null)
        //{
        //    transform.GetChild(0).gameObject.AddComponent(objectCollider.GetType());
        //    snapCollider = transform.GetChild(0).GetComponent<Collider>();
        //}
        //else
        //{
        //    snapCollider = transform.GetComponentInChildren<Collider>();
        //}
    }

    protected virtual void ModifyTrigger()
    {
        // TODO für alle Colliderformen bauen  --> Validate-Ticket
        if (objectCollider.GetType() == typeof(BoxCollider))
        {
            BoxCollider boxCollider = (BoxCollider)objectCollider;
            boxCollider.size = new Vector3(1.2f, 1f, 2f);
        }
    }
}
