using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : Interactable
{
    private List<Transform> touchHandles = new List<Transform>();
    private bool isTriggered = false;
    private InteractionManager interactionManager = null;
    private TouchObjectInteraction currentInteraction = null;

    // TODO schön DEBUG im besten Fall nicht per SerializeField
    [SerializeField] private RandomCondition random = null;

    public List<Transform> TouchHandles { get => touchHandles; }
    public bool IsTriggered { get => isTriggered; }

    protected override void Start()
    {
        base.Start();

        interactionManager = FindObjectOfType<InteractionManager>();
    }

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

        if(touchHandles.Count <= 0)
        {
            Debug.LogError("No Touchhandles found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharController>() != null)
        {
            isTriggered = true;
            currentInteraction = (TouchObjectInteraction)interactionManager.CurrentInteraction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharController>() != null)
        {
            isTriggered = false;
            random.IsExecuted = false;
        }
    }
}
