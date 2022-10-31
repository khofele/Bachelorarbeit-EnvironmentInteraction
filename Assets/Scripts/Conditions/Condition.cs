using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    protected CharController charController = null;

    protected virtual void Start()
    {
        charController = GetComponentInParent<CharController>();
    }

    public abstract bool CheckCondition();
}
