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

    private void LeanOnObject()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(snapCollider.transform.position);
        Vector3 objectClosestPoint = snapCollider.ClosestPoint(playerClosestPoint);
        offset = objectClosestPoint - playerClosestPoint;

        if (offset.magnitude < snapDistance)
        {
            charController.transform.position += offset;

            charController.IsLeaning = true;
            ExecuteAnimation();
        }

        RaycastHit hit;

        if ((Physics.Raycast(charController.transform.position, charController.transform.forward, out hit, 1.5f))
            && charController.IsLeaning == true)
        {
            charController.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else if (Physics.Raycast(charController.transform.position, -charController.transform.forward, out hit, 1.5f) && charController.IsLeaning == true)
        {
            charController.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            charController.transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized, Camera.main.transform.up);
            isInteracting = false;
            StopAnimation();
        }
    }

    public override void Start()
    {
        base.Start();
        matchingInteractable = typeof(Leanable);
        playerCollider = charController.GetComponent<CharacterController>();
    }

    public override void Update()
    {
        base.Update();

        if (isInteracting == true)
        {
            LeanOnObject();

            if (TerminationCondition())
            {
                isInteracting = false;
                charController.IsLeaning = false;
                iKController.IsIkActive = false;
                StopAnimation();
            }
        }
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
    public abstract bool TerminationCondition();
}
