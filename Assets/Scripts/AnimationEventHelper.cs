using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    private ThrowObjectInteraction throwObjectInteraction = null;

    private void Start()
    {
        throwObjectInteraction = GetComponentInChildren<ThrowObjectInteraction>();
    }

    public void ExecuteThrow()
    {
        throwObjectInteraction.Throw();
    }
}
