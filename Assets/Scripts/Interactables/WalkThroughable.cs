using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkThroughable : Interactable
{
    protected bool isTriggered = false;
    protected InteractionManager interactionManager = null;
    protected WalkThroughInteraction currentInteraction = null;

    public bool IsTriggered { get => isTriggered; }

    protected override void Start()
    {
        base.Start();

        interactionManager = FindObjectOfType<InteractionManager>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = true;
            SetCurrentInteraction();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = false;
        }
    }

    protected abstract void SetCurrentInteraction();
}
