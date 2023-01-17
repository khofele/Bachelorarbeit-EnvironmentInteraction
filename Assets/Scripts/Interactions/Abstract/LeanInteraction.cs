using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeanInteraction : InterruptibleInteraction
{
    protected Collider snapCollider = null;
    protected Collider charCollider = null;
    protected Leanable currentLeanableObject = null;
    protected float snapDistance = 1.5f;
    protected Vector3 offset;

    protected virtual void LeanOnObject()
    {
        Vector3 charClosestPoint = charCollider.ClosestPoint(snapCollider.transform.position);
        Vector3 objectClosestPoint = snapCollider.ClosestPoint(charClosestPoint);
        offset = objectClosestPoint - charClosestPoint;

        if (offset.magnitude < snapDistance)
        {
            interactionManager.IsLeaningSnapping = true;
            charController.transform.position += offset;
            finalIKController.IsIkActive = true;
            SetLeanBool(true);
            ExecuteAnimation();
        }
    }

    protected virtual void DetectObject()
    {
        Ray rayFront = new Ray(charController.transform.position, charController.transform.forward);
        Ray rayBack = new Ray(charController.transform.position, -charController.transform.forward);
        Ray rayRight = new Ray(charController.transform.position, charController.transform.right);
        Ray rayLeft = new Ray(charController.transform.position, -charController.transform.right);

        RaycastHit hit;

        if (((Physics.Raycast(rayFront, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayBack, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayRight, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayLeft, out hit, 1f, LayerMask.NameToLayer("Checkbox"))))
            && CheckLeaningBool())
        {
            if (hit.transform.gameObject.GetComponent<Interactable>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
            else if(hit.transform.gameObject.GetComponent<InteractableParentManager>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
        }
        else
        {
            charController.transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized, Camera.main.transform.up);
            isInteractionRunning = false;
            StopAnimation();
        }
    }

    protected override void Start()
    {
        base.Start();
        charCollider = charController.GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        base.Update();

        ExecuteLeanInteraction();
    }

    protected virtual void ExecuteLeanInteraction()
    {
        if (isInteractionRunning == true)
        {
            if(Vector3.Distance(charController.transform.position, snapCollider.ClosestPoint(charController.transform.position)) > 0.3f)
            {
                LeanOnObject();
            }
            else
            {
                interactionManager.IsLeaningSnapping = false;
            }

            DetectObject();

            if (CheckTerminationCondition())
            {
                ResetInteraction();
            }
        }
    }

    protected virtual void ResetValues()
    {
        isInteractionRunning = false;
        finalIKController.IsIkActive = false;
    }

    protected abstract void ExecuteAnimation();
    protected abstract void StopAnimation();
    protected abstract bool CheckTerminationCondition();
    protected abstract void ResetCharacter();
    protected abstract void SetLeanBool(bool value);
    protected abstract bool CheckLeaningBool();
    
    public override void ResetInteraction()
    {
        isInteractionRunning = false;
        isTriggeredByInterruptibleInteraction = false;
        ResetCharacter();
        currentLeanableObject = null;
        ResetValues();
        StopAnimation();
        interactionManager.IsLeaningSnapping = false;
        interactionManager.SetLastInteraction();
    }
    
    public override void ExecuteInteraction()
    {
        if (isInteractionRunning == false)
        {
            interactionManager.IsLeaningSnapping = true;
        }

        base.ExecuteInteraction();
        currentLeanableObject = (Leanable)interactableManager.CurrentInteractable;
        snapCollider = currentLeanableObject.SnapCollider;
        LeanOnObject();
    }
}
