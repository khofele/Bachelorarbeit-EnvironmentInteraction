using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTriggerCheckCharacter : MonoBehaviour
{
    private InteractionManager interactionManager = null;
    private InteractableManager interactableManager = null;
    private CharController charController = null;

    private void Start()
    {
        interactableManager = FindObjectOfType<InteractableManager>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        charController = GetComponent<CharController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null && interactionManager.IsInteractionTriggered == false)
        {
            interactableManager.CurrentInteractable = other.gameObject.GetComponent<Interactable>();

            if (interactableManager.CurrentInteractable.GetComponent<InteractableTriggerProperty>() != null)
            {
                if (interactableManager.CurrentInteractable.GetComponent<InteractableTriggerProperty>().IsActivatedFromEverySide == false)
                {
                    EnableBoxes(interactableManager.CurrentInteractable);
                }
                else
                {
                    interactionManager.IsInteractionTriggered = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionManager.IsInteractionTriggered = false;
    }

    private void EnableBoxes(Interactable currentInteractable)
    {
        foreach (TriggerCheck checkObject in currentInteractable.GetComponent<InteractableTriggerProperty>().TriggerChecks)
        {
            checkObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void DisableBoxes()
    {
        foreach (TriggerCheck checkObject in charController.TriggerCheckManager.AllChecks)
        {
            checkObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
