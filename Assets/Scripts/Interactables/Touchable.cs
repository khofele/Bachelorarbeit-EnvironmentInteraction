using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : WalkThroughable
{
    private List<Transform> touchHandles = new List<Transform>();
    private RandomCondition randomCondition = null;

    public List<Transform> TouchHandles { get => touchHandles; }

    protected override void Validate()
    {
        base.Validate();

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform transform in children)
        {
            if (transform.CompareTag("TouchHandle"))
            {
                touchHandles.Add(transform);
            }
        }

        if (touchHandles.Count <= 0)
        {
            Debug.LogError("No Touchhandles found!");
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        randomCondition = interactionManager.CurrentInteraction.gameObject.GetComponent<RandomCondition>();

        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = false;

            if(randomCondition != null)
            {
                randomCondition.IsExecuted = false;
            }
        }
    }

    protected override void SetCurrentInteraction()
    {
        currentInteraction = (TouchObjectInteraction)currentInteraction;
        currentInteraction = (TouchObjectInteraction)interactionManager.CurrentInteraction;
    }
}
