using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private InteractionManager interactionManager = null;
    private GeneralTriggerCheckCharacter triggerCheck = null;

    private void Start()
    {
        interactionManager = transform.parent.transform.parent.GetComponentInChildren<InteractionManager>();
        triggerCheck = transform.parent.GetComponentInParent<GeneralTriggerCheckCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            interactionManager.IsInteractionTriggered = true;
            triggerCheck.DisableBoxes();
        }
    }
}
