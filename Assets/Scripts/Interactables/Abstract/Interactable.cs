using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InteractableTriggerProperty))]
public abstract class Interactable : MonoBehaviour
{
    protected InteractableManager interactableManager = null;
    protected bool isInteractableTriggered = false;

    public bool IsInteractableTriggered { get => isInteractableTriggered; }

    protected virtual void Start()
    {
        interactableManager = FindObjectOfType<InteractableManager>();

        Validate();
    }

    protected virtual void Validate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CharController>() != null)
        {
            isInteractableTriggered = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isInteractableTriggered = false;
        }
    }
}
