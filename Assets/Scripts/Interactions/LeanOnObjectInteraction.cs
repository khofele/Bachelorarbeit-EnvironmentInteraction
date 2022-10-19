using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanOnObjectInteraction : Interaction
{
    private Collider snapCollider = null;
    private Collider playerCollider = null;
    private Leanable currentLeanableObject = null;
    private float snapDistance = 1f;
    private Vector3 offset;

    private void LeanOnObject()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(snapCollider.transform.position);
        Vector3 objectClosestPoint = snapCollider.ClosestPoint(playerClosestPoint);
        offset = objectClosestPoint - playerClosestPoint;

        if (offset.magnitude < snapDistance)
        {
            charController.transform.position += offset;

            charController.IsLeaning = true;
            animationManager.ExecuteCrouchAndLeanAnimation();
            animationManager.ExecuteCrouchAndLeanAnimation(charController.XAxis);
        }

        RaycastHit hit;

        if ((Physics.Raycast(charController.transform.position, charController.transform.forward, out hit, 1.5f))
            && charController.IsLeaning == true)
        {
            charController.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else if(Physics.Raycast(charController.transform.position, -charController.transform.forward, out hit, 1.5f) && charController.IsLeaning == true)
        {
            charController.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            charController.transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized, Camera.main.transform.up);
            isInteracting = false;
            animationManager.StopCrouchAndLeanAnimation();
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

            if (offset.magnitude > snapDistance || charController.IsCrouching == false || isInteracting == false)
            {
                isInteracting = false;
                charController.IsLeaning = false;
                iKController.IsIkActive = false;
                animationManager.StopCrouchAndLeanAnimation();
            }
        }
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        currentLeanableObject = (Leanable) interactableManager.CurrentInteractable;
        snapCollider = currentLeanableObject.SnapCollider;
    }

    public override void ResetInteraction()
    {
        currentLeanableObject = null;
    }
}
