using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckMultipleInteractable : MonoBehaviour
{
    private InteractableManager interactableManager = null;

    private void Start()
    {
        interactableManager = FindObjectOfType<InteractableManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactableManager.CurrentMultipleInteractable != null)
        {
            interactableManager.CurrentMultipleInteractable = null;
        }
    }
}
