using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverInteraction : Interaction
{
    private Collider snapCollider = null;
    private Collider playerCollider = null;
    private Leanable currentLeanableObject = null;
    private float snapDistance = 1f;
    private Vector3 offset;

    // TODO ggf. mit Lean mergen
    private void Cover()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(snapCollider.transform.position);
        Vector3 objectClosestPoint = snapCollider.ClosestPoint(playerClosestPoint);
        offset = objectClosestPoint - playerClosestPoint;

        if (offset.magnitude < snapDistance)
        {
            charController.transform.position += offset;

            charController.IsLeaning = true;
            animationManager.ExecuteCoverAnimation();
            animationManager.ExecuteCoverAnimation(charController.XAxis);
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
            animationManager.StopCoverAnimation();
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
            Cover();

            // TODO Abbruchbedingung?
            if (offset.magnitude > snapDistance || isInteracting == false)
            {
                isInteracting = false;
                charController.IsLeaning = false;
                iKController.IsIkActive = false;
                animationManager.StopCoverAnimation();
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
}
