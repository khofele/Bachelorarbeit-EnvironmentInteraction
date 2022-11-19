using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    protected CharController charController = null;
    protected InteractionManager interactionManager = null;
    protected InteractableManager interactableManager = null;

    protected virtual void Start()
    {
        charController = GetComponentInParent<CharController>();
        interactionManager = FindObjectOfType<InteractionManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
    }

    public abstract bool CheckCondition();
}
