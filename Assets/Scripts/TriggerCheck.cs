using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private InteractionManager interactionManager = null;
    private InteractableManager interactableManager = null;

    private void Start()
    {
        interactableManager = FindObjectOfType<InteractableManager>();
        interactionManager = GetComponentInChildren<InteractionManager>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null && interactionManager.IsInteractionTriggered == false)
        {
            interactableManager.CurrentInteractable = other.gameObject.GetComponent<Interactable>();
            interactionManager.IsInteractionTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionManager.IsInteractionTriggered = false;
    }
}
