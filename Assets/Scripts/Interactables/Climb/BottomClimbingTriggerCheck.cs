using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomClimbingTriggerCheck : MonoBehaviour
{
    private Climbable climbable = null;
    private InteractionManager interactionManager = null;

    private void Start()
    {
        climbable = GetComponentInParent<Climbable>();
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(interactionManager.CurrentInteraction != null)
        {
            if(interactionManager.CurrentInteraction.GetType() == typeof(ClimbInteraction))
            {
                if(interactionManager.CurrentInteraction.IsInteractionRunning == false && other.gameObject.GetComponent<CharController>() != null)
                {
                    climbable.TriggerCount = 0;
                }
            }
        }
    }
}
