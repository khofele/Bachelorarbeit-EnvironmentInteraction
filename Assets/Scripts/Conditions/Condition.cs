using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    public abstract bool CheckCondition();

    public abstract void ResetCondition();
}
