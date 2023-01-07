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
        if (other.gameObject.GetComponent<InteractableParentManager>() != null)
        {
            interactableManager.CurrentInteractableParent = other.gameObject.GetComponent<InteractableParentManager>();

            CheckInteractableProperties();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(interactableManager.CurrentInteractableParent != null)
        {
            foreach (Interactable interactable in interactableManager.CurrentInteractableParent.Interactables)
            {
                interactionManager.GetCurrentInteraction(interactable).IsInteractionTriggered = false;
            }
        }

        foreach (RandomCondition randomCondition in GetComponentsInChildren<RandomCondition>())
        {
            randomCondition.IsExecuted = false;
        }
    }

    private void CheckInteractableProperties()
    {
        foreach(Interactable interactable in interactableManager.CurrentInteractableParent.Interactables)
        {
            if (interactable.GetComponent<InteractableTriggerProperty>().IsActivatedFromEverySide == false && interactable.IsInteractableTriggered == true)
            {
                if(CheckTriggerCheckResults(interactable) == true) 
                {
                    interactionManager.GetCurrentInteraction(interactable).IsInteractionTriggered = true;
                }
            }
            else if (interactable.IsInteractableTriggered == true)
            {
                interactionManager.GetCurrentInteraction(interactable).IsInteractionTriggered = true;
            }
        }
    }

    private bool CheckTriggerCheckResults(Interactable currentInteractable)
    {
        foreach (TriggerCheck checkObject in currentInteractable.GetComponent<InteractableTriggerProperty>().TriggerChecks)
        {
            if(checkObject.IsTriggered == true)
            {
                return true;
            }
        }

        return false;
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
            checkObject.IsTriggered = false;
            checkObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
