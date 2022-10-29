using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    protected virtual void Start()
    {
        Validate();
    }

    protected virtual void Validate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}
