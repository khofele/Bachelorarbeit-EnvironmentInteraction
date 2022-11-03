using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectInteraction : Interaction
{
    private GameObject enemy = null;
    private SphereCollider sphereCollider = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && enemy == null)
        {
            enemy = other.gameObject;
        }
    }

    protected override void Start()
    {
        base.Start();
        sphereCollider = GetComponent<SphereCollider>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Interactable"), LayerMask.NameToLayer("Checkbox"));
        sphereCollider.enabled = false;
    }

    protected override void Update()
    {
        if (CheckTrigger() == true)
        {
            if (CheckMatchingInteractable() == true)
            {
                if (CheckConditions() == true)
                {
                    ExecuteInteraction();
                    ResetInteraction();
                }
            }
        }
    }

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        //Throw(); ist AnimationEvent
    }

    public void Throw()
    {
        if (interactableManager.CurrentInteractable != null)
        {
            interactableManager.CurrentInteractable.transform.SetParent(iKController.Interactables.transform, true);

            // Throw
            // TODO in IKController --> Ticket: IK-Controller Refactoring
            charController.Animator.SetLookAtWeight(0.8f);
            Rigidbody rigidBody = interactableManager.CurrentInteractable.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            if (enemy != null)
            {
                Vector3 direction = enemy.transform.position - charController.transform.position;
                rigidBody.AddForce(direction.normalized * 15 + transform.forward * 0.5f, ForceMode.Impulse);
            }
            else
            {
                rigidBody.AddForce(Camera.main.transform.forward * 15 + transform.up, ForceMode.Impulse);
            }

            animationManager.StopThrowAnimation();
            iKController.IsIkActive = false;
            isInteractionRunning = false;
        }
    }

    public void EnableCollider()
    {
        sphereCollider.enabled = true;
    }

    public void DisableCollider()
    {
        sphereCollider.enabled = false;
    }

    protected override void ResetInteraction()
    {
        enemy = null;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Throwable);
    }
}
