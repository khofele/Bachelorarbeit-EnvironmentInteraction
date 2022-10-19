using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leanable : Interactable
{
    private Collider snapCollider = null;
    private Collider objectCollider = null;
    public Collider SnapCollider { get => snapCollider; }

    public override void Validate()
    {
        base.Validate();
        GetComponent<Rigidbody>().isKinematic = true;

        if (transform.childCount == 0)
        {
            GameObject emptyGameObject = new GameObject();
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
}
