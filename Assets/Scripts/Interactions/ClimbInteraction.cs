using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbInteraction : Interaction
{
    private Climbable currentClimbable = null;
    private Vector3 moveDirection = Vector3.zero;

    // DEBUG
    private bool isTerminating = false;
    // DEBUG

    public Climbable CurrentClimbable { get => currentClimbable; }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Climbable);
    }

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        currentClimbable = (Climbable)interactableManager.CurrentInteractable;
    }

    protected override void Update()
    {
        if (CheckTrigger() == true)
        {
            if (CheckMatchingInteractable() == true)
            {
                if (CheckOtherInteractionsRunning() == true)
                {
                    if (CheckConditions() == true)
                    {
                        ExecuteInteraction();
                    }
                }
            }
        }

        ExecuteClimbInteraction();

        if(isTerminating == true)
        {
            interactionManager.IsClimbingSnapping = true;
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, new Vector3(charController.transform.position.x, currentClimbable.TopTransform.position.y, charController.transform.position.z), 1 * Time.deltaTime);

            Debug.Log("Snap");

            if (Vector3.Distance(charController.transform.position, new Vector3(charController.transform.position.x, currentClimbable.TopTransform.position.y, charController.transform.position.z)) < 0.001f)
            {
                Debug.Log("feddich");
                isTerminating = false;
                interactionManager.IsClimbing = false;
                interactionManager.IsClimbingSnapping = false;
            }
        }

    }

    private void ExecuteClimbInteraction()
    {
        if (isInteractionRunning == true)
        {
            animationManager.ExecuteClimb();
            interactionManager.IsClimbing = true;

            DetectWall();

            if (CheckTerminationCondition())
            {
                ResetInteraction();
            }
        }
    }

    private bool CheckTerminationCondition()
    {
        if (currentClimbable.TriggerCount % 2 == 0 && currentClimbable.TriggerCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void ResetInteraction()
    {
        base.ResetInteraction();

        if (currentClimbable.TriggerCheck.IsTopReached == true)
        {
            // TODO KARO vollns hoch klettern und dann Interaktion beenden --> Transform erstellt, dessen Höhe genommen werden kann, um den Charakter hochzusnappen?
            animationManager.StopClimb();
            animationManager.ExecuteClimbToTop();

            isTerminating = true; 


            // TODO KARO erst zurücksetzen, wenn Char ganz oben ist
            //interactionManager.IsClimbing = false;
        }
        else
        {
            animationManager.StopClimb();
            interactionManager.IsClimbing = false;
        }
    }

    private void DetectWall()
    {
        Ray rayFront = new Ray(charController.transform.position, charController.transform.forward);
        Ray rayRight = new Ray(charController.transform.position, charController.transform.right);
        Ray rayLeft = new Ray(charController.transform.position, -charController.transform.right);

        RaycastHit hit;

        if (((Physics.Raycast(rayFront, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayRight, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayLeft, out hit, 1f, LayerMask.NameToLayer("Checkbox"))))
            && interactionManager.IsClimbing == true)
        {
            if (hit.transform.gameObject.GetComponent<Climbable>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(-hit.normal);
            }
        }
        else
        {
            ResetInteraction();
        }
    }
}
