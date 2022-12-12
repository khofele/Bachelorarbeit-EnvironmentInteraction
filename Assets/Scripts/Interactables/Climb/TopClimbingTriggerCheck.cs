using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopClimbingTriggerCheck : MonoBehaviour
{
    private bool isOnTopOfWall = false;
    private InteractionManager interactionManager = null;
    private ClimbInteraction climbInteraction = null;

    public bool IsOnTopOfWall { get => isOnTopOfWall; set => isOnTopOfWall = value; }

    private void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isOnTopOfWall = true;

            climbInteraction = (ClimbInteraction)interactionManager.CurrentInteraction;
            climbInteraction.IsInteractionTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isOnTopOfWall = false;
        }
    }
}
