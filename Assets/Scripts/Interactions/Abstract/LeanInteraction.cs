using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeanInteraction : Interaction
{
    public Collider snapCollider = null;
    public Collider playerCollider = null;
    public Leanable currentLeanableObject = null;
    public float snapDistance = 1f;
    public Vector3 offset;

    public virtual void LeanOnObject()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(snapCollider.transform.position);
        Vector3 objectClosestPoint = snapCollider.ClosestPoint(playerClosestPoint);
        offset = objectClosestPoint - playerClosestPoint;

        if (offset.magnitude < snapDistance)
        {
            charController.transform.position += offset;

            SetLeanBool(true);
            ExecuteAnimation();
        }

        Ray rayFront = new Ray(charController.transform.position, charController.transform.forward);
        Ray rayBack = new Ray(charController.transform.position, -charController.transform.forward);
        Ray rayRight = new Ray(charController.transform.position, charController.transform.right);
        Ray rayLeft = new Ray(charController.transform.position, -charController.transform.right);

        RaycastHit hit;

        if (((Physics.Raycast(rayFront, out hit, 1f)) || (Physics.Raycast(rayBack, out hit, 1f)) || (Physics.Raycast(rayRight, out hit, 1f)) || (Physics.Raycast(rayLeft, out hit, 1f)))
            && CheckLeaningBool())
        {
            if (hit.transform.gameObject.GetComponent<Interactable>() != null)
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

    public override void Start()
    {
        base.Start();
        playerCollider = charController.GetComponent<CharacterController>();
    }

    public override void Update()
    {
        base.Update();
        ExecuteLeanInteraction();
    }

    public virtual void ExecuteLeanInteraction()
    {
        if (isInteractionRunning == true)
        {
            LeanOnObject();

            if (CheckTerminationCondition())
            {
                ResetValues();
                ResetCharacter();
                StopAnimation();
            }
        }
    }

    public virtual void ResetValues()
    {
        isInteractionRunning = false;
        iKController.IsIkActive = false;
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        currentLeanableObject = (Leanable)interactableManager.CurrentInteractable;
        snapCollider = currentLeanableObject.SnapCollider;
    }

    public override void ResetInteraction()
    {
        currentLeanableObject = null;
    }

    public abstract void ExecuteAnimation();
    public abstract void StopAnimation();
    public abstract bool CheckTerminationCondition();
    public abstract void ResetCharacter();
    public abstract void SetLeanBool(bool value);
    public abstract bool CheckLeaningBool();
}
