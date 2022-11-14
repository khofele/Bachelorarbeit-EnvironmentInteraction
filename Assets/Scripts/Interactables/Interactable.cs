using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InteractableTriggerProperty))]
public abstract class Interactable : MonoBehaviour
{
    protected virtual void Start()
    {
        Validate();
    }

    protected virtual void Validate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
