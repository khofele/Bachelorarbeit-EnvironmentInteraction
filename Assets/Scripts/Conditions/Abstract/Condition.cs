using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    protected CharController charController = null;
    protected InteractionManager interactionManager = null;

    protected virtual void Start()
    {
        charController = GetComponentInParent<CharController>();
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    public abstract bool CheckCondition();
}
