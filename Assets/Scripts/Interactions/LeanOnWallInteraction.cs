using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanOnWallInteraction : Interaction
{
    [SerializeField] private Collider wallCollider = null;
    [SerializeField] private BoxCollider walkCollider = null;
    [SerializeField] private Collider playerCollider = null;
    private float snapDistance = 1f;
    private Vector3 offset;

    // TODO Collider von Wand bekommen
    // TODO Wand muss drei Collider haben --> Leanable-Skript

    private void LeanOnWall()
    {
        Vector3 playerClosestPoint = playerCollider.ClosestPoint(wallCollider.transform.position);
        Vector3 wallClosestPoint = wallCollider.ClosestPoint(playerClosestPoint);
        offset = wallClosestPoint - playerClosestPoint;

        if(offset.magnitude < snapDistance)
        {
            charController.transform.position += offset;
        }
    }

    public override void Start()
    {
        base.Start();
        Physics.IgnoreCollision(playerCollider, walkCollider);
        matchingInteractable = typeof(Leanable);
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
        base.ExecuteInteraction();
    }

    public override void ResetInteraction()
    {
        // TODO Wandcollider resetten
    }
}
