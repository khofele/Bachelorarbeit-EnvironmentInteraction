using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbInteraction : Interaction
{
    private Climbable currentClimbable = null;
    private bool isClimbingUp = false;
    private bool isClimbingDown = false;

    public Climbable CurrentClimbable { get => currentClimbable; }

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

    private void DetectWall()
    {
        Ray rayFront = new Ray(charController.transform.position, charController.transform.forward);
        Ray rayBack = new Ray(charController.transform.position, -charController.transform.forward);
        Ray rayRight = new Ray(charController.transform.position, charController.transform.right);
        Ray rayLeft = new Ray(charController.transform.position, -charController.transform.right);

        RaycastHit hit;

        if (((Physics.Raycast(rayFront, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayBack, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayRight, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayLeft, out hit, 1f, LayerMask.NameToLayer("Checkbox"))))
            && interactionManager.IsClimbing == true)
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.GetComponent<Climbable>() != null)
            {
                Debug.Log("hallo");
                charController.transform.rotation = Quaternion.LookRotation(-hit.normal);
            }
            else if (hit.transform.gameObject.GetComponent<InteractableParentManager>() != null)
            {
                Debug.Log("ja moin");
                charController.transform.rotation = Quaternion.LookRotation(-hit.normal);
            }
        }
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);
        isClimbingUp = false;
        interactionManager.IsClimbing = false;
        interactionManager.IsClimbingSnapping = false;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Climbable);
    }

    protected override void Update()
    {
        base.Update();

        if (isClimbingDown == true)
        {
            if (Vector3.Distance(charController.transform.position, new Vector3(charController.transform.position.x, currentClimbable.ClimbDownTransform.position.y, currentClimbable.ClimbDownTransform.position.z)) < 0.01f)
            {
                isClimbingDown = false;
                interactionManager.IsClimbingSnapping = false;
            }
            else
            {
                interactionManager.IsClimbingSnapping = true;
                charController.transform.position = Vector3.MoveTowards(charController.transform.position, new Vector3(charController.transform.position.x, currentClimbable.ClimbDownTransform.position.y, currentClimbable.ClimbDownTransform.position.z), 1 * Time.deltaTime);
            }
        }

        if (isClimbingDown == false)
        {
            ExecuteClimbInteraction();
        }

        if (isClimbingUp == true && charController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Climbing To Top"))
        {
            if (Vector3.Distance(charController.transform.position, new Vector3(charController.transform.position.x, currentClimbable.TopTransform.position.y, currentClimbable.TopTransform.position.z)) < 0.01f)
            {
                StartCoroutine(WaitAndReset());
            }
            else
            {
                interactionManager.IsClimbingSnapping = true;
                charController.transform.position = Vector3.MoveTowards(charController.transform.position, new Vector3(charController.transform.position.x, currentClimbable.TopTransform.position.y, currentClimbable.TopTransform.position.z), 1 * Time.deltaTime);
            }
        }
    }

    public override void ResetInteraction()
    {

        animationManager.StopClimb();

        if (currentClimbable.TopClimbingTriggerCheck.IsOnTopOfWall == true)
        {
            animationManager.ExecuteClimbToTop();
            isClimbingUp = true;
            animationManager.SetIsOnTop(true);
            isInteractionRunning = false;
            finalIKController.IsIkActive = false;
            isInteractionTriggered = true;
        }
        else
        {
            interactionManager.IsClimbing = false;
            animationManager.SetIsOnTop(false);

            base.ResetInteraction();
        }
        currentClimbable.TriggerCount = 0;

    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        currentClimbable = (Climbable)interactableManager.CurrentInteractable;

        if (currentClimbable.TopClimbingTriggerCheck.IsOnTopOfWall == true)
        {
            currentClimbable.TriggerCount++;
            animationManager.SetIsOnTop(true);
            animationManager.ExecuteClimbDown();
            finalIKController.IsIkActive = false;
            isClimbingDown = true;
        }
        else
        {
            animationManager.SetIsOnTop(false);
        }

        if(currentClimbable.TriggerCount <= 0)
        {
            currentClimbable.TriggerCount = 1;
        }
    }
}
