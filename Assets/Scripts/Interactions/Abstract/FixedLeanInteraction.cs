using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FixedLeanInteraction : Interaction
{
    protected FixedLeanable currentLeanable = null;
    protected Leanable currentLeanableObject = null;
    protected Collider snapCollider = null;
    protected Collider playerCollider = null;
    protected float snapDistance = 1.5f;
    protected Vector3 offset;
    private bool isLeaningFixedObject = false;
    private bool isTerminating = false;
    private bool isSnapping = false;

    public bool IsSnapping { get => isSnapping; }

    protected override void Start()
    {
        base.Start();
        playerCollider = charController.GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        base.Update();

        ExecuteLeanInteraction();
    }

    public override void ExecuteInteraction()
    {
        finalIKController.IsIkActive = true;
        interactionManager.CurrentInteraction = this;
        isInteractionRunning = true;
        interactionManager.SetLastInteraction();
        SetCurrentInteractable();
        currentLeanableObject = (Leanable)interactableManager.CurrentInteractable;
        snapCollider = currentLeanableObject.SnapCollider;
        SetLeanBool(true);

        LeanOnObject();
        DetectObject();
    }

    protected void ExecuteLeanInteraction()
    {
        if (isInteractionRunning == true && isTerminating == false)
        {
            if (isLeaningFixedObject == true)
            {
                if (Vector3.Distance(charController.transform.position, snapCollider.ClosestPoint(charController.transform.position)) > 0.35f)
                {
                    LeanOnObject();
                }
                else
                {
                    interactionManager.IsLeaningSnapping = false;
                }

                DetectObject();
            }
            else
            {

                SnapToFixedObject();
            }

            if (CheckTerminationCondition())
            {
                isTerminating = true;
                isInteractionRunning = false;
            }
        }

        if (isTerminating == true)
        {
            charController.XAxis *= 0;
            ResetCharacter();
            StopAnimation();
        }
    }

    protected virtual void LeanOnObject()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(snapCollider.transform.position);
        Vector3 objectClosestPoint = snapCollider.ClosestPoint(playerClosestPoint);
        offset = objectClosestPoint - playerClosestPoint;

        if (offset.magnitude < snapDistance)
        {
            interactionManager.IsLeaningSnapping = true;
            charController.transform.position += offset;
            SetLeanBool(true);
            ExecuteAnimation();
        }
    }

    protected virtual void ExecuteAnimation()
    {
        animationManager.ExecuteStandLeanAnimation(charController.XAxis);
    }

    protected virtual void StopAnimation()
    {
        animationManager.StopStandLeanAnimation();
    }

    protected virtual bool CheckTerminationCondition()
    {
        if (currentLeanable.TriggerCount % 2 == 0 && currentLeanable.TriggerCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void ResetValues()
    {
        isInteractionRunning = false;
        finalIKController.IsIkActive = false;
        isLeaningFixedObject = false;
        currentLeanable.TriggerCount = 0;
        isCharInteracting = false;
        isTerminating = false;
        SetLeanBool(false);
    }

    protected override void ResetInteraction()
    {
        ResetValues();
        currentLeanableObject = null;
        interactionManager.IsLeaningSnapping = false;
        interactionManager.SetLastInteraction();
    }

    protected virtual void ResetCharacter()
    {
        Transform resetTransform = CheckResetTransform();
        Vector3 position = new Vector3(resetTransform.position.x, charController.transform.position.y, resetTransform.position.z);

        interactionManager.IsFixedSnapping = true;
        if (Vector3.Distance(charController.transform.position, position) < 0.001f)
        {
            interactionManager.IsFixedSnapping = false;
            ResetInteraction();
            StopAnimation();
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, position, 2 * Time.deltaTime);
        }
    }

    private Transform CheckSnapTransform()
    {
        SetCurrentLeanable();
        float distanceTransformOne = Vector3.Distance(charController.transform.position, currentLeanable.SnapTransformOne.position);
        float distanceTransformTwo = Vector3.Distance(charController.transform.position, currentLeanable.SnapTransformTwo.position);

        if (distanceTransformOne < distanceTransformTwo)
        {
            return currentLeanable.SnapTransformOne;
        }
        else
        {
            return currentLeanable.SnapTransformTwo;
        }
    }

    private Transform CheckResetTransform()
    {
        SetCurrentLeanable();
        float distanceTransformOne = Vector3.Distance(charController.transform.position, currentLeanable.ResetTransformOne.position);
        float distanceTransformTwo = Vector3.Distance(charController.transform.position, currentLeanable.ResetTransformTwo.position);

        if (distanceTransformOne < distanceTransformTwo)
        {
            return currentLeanable.ResetTransformOne;
        }
        else
        {
            return currentLeanable.ResetTransformTwo;
        }
    }

    private void SnapToFixedObject()
    {
        Transform snapTransform = CheckSnapTransform();
        Vector3 position = new Vector3(snapTransform.position.x, charController.transform.position.y, snapTransform.position.z);

        interactionManager.IsFixedSnapping = true;
        if (Vector3.Distance(charController.transform.position, position) < 0.01f)
        {
            isLeaningFixedObject = true;
            isCharInteracting = true;
            interactionManager.IsFixedSnapping = false;
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, position, 3 * Time.deltaTime);
        }

        DetectObject();

        ExecuteAnimation();
    }

    protected virtual void DetectObject()
    {
        Ray rayFront = new Ray(charController.transform.position, charController.transform.forward);
        Ray rayBack = new Ray(charController.transform.position, -charController.transform.forward);
        Ray rayRight = new Ray(charController.transform.position, charController.transform.right);
        Ray rayLeft = new Ray(charController.transform.position, -charController.transform.right);

        RaycastHit hit;


        // ignores children of interactables!
        if ((Physics.Raycast(rayFront, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayBack, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayRight, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayLeft, out hit, 1f, LayerMask.NameToLayer("Checkbox"))))
        {
            if (hit.transform.gameObject.GetComponent<FixedLeanable>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(-hit.transform.forward);
            }
            else if (hit.transform.gameObject.GetComponent<InteractableParentManager>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
        }
    }

    protected abstract void SetCurrentLeanable();
    protected abstract void SetLeanBool(bool value);
    protected abstract bool CheckLeaningBool();
}
