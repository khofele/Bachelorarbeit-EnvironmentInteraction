using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : WalkThroughable
{
    private List<Transform> touchHandles = new List<Transform>();

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
        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = false;
        }
    }
}
