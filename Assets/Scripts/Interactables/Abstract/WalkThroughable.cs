using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkThroughable : Interactable
{
    protected bool isTriggered = false;
    protected InteractionManager interactionManager = null;

    public bool IsTriggered { get => isTriggered; }

    protected override void Start()
    {
        base.Start();

        interactionManager = FindObjectOfType<InteractionManager>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = false;
        }
    }
}
