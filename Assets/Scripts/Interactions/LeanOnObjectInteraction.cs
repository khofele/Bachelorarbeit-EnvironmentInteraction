using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanOnObjectInteraction : Interaction
{
    private Collider objectCollider = null;
    private Collider walkCollider = null;
    private Collider playerCollider = null;
    private Leanable currentLeanableObject = null;
    private float snapDistance = 1f;    // TODO evtl. snap distance noch balancen
    private Vector3 offset;

    private void LeanOnWall()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(objectCollider.transform.position);
        Vector3 wallClosestPoint = objectCollider.ClosestPoint(playerClosestPoint);
        offset = wallClosestPoint - playerClosestPoint;

        if(offset.magnitude < snapDistance)
        {
            charController.transform.position += offset;
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
            LeanOnWall();
                
            if (offset.magnitude > snapDistance || charController.IsCrouching == false)
            {
                isInteracting = false;
            }
        }
    }

    public override void ExecuteInteraction()
    {
        Debug.Log("Execute LeanOnWallInteraction");
        interactionManager.CurrentInteraction = this;
        isInteracting = true;
        currentLeanableObject = (Leanable) interactableManager.CurrentInteractable;
        objectCollider = currentLeanableObject.ParentCollider;
        walkCollider = currentLeanableObject.WalkCollider;
        base.ExecuteInteraction();
    }

    public override void ResetInteraction()
    {
        currentLeanableObject = null;
    }
}
