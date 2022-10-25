using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    public CharController charController = null;
    public virtual void Start()
    {
        charController = GetComponentInParent<CharController>();
    }
    public abstract bool CheckCondition();
}
