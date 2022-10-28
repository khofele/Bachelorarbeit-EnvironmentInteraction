using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    public virtual void Start()
    {
        Validate();
    }

    public virtual void Validate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}
