using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectInteraction : Interaction
{
    private CharController charController = null;
    private IKController iKController = null;
    private GameObject enemy = null;
    private SphereCollider sphereCollider = null;
    private InteractableManager interactableManager = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && enemy == null)
        {
            enemy = other.gameObject;
        }
    }

    public override void Start()
    {
        base.Start();
        charController = GetComponentInParent<CharController>();
        iKController = GetComponentInParent<IKController>();
        sphereCollider = GetComponent<SphereCollider>();
        interactableManager = FindObjectOfType<InteractableManager>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Interactable"), LayerMask.NameToLayer("Checkbox"));
        sphereCollider.enabled = false;
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Debug.Log("Execute ThrowObjectInteraction");
        //Throw(); ist AnimationEvent
        iKController.IsIkActive = true;
        interactionManager.CurrentInteraction = this;
    }

    public void Throw()
    {
        if (interactableManager.CurrentInteractable != null)
        {
            interactableManager.CurrentInteractable.transform.SetParent(iKController.Interactables.transform, true);

            // Throw
            charController.Animator.SetLookAtWeight(0.8f);
            Rigidbody rigidBody = interactableManager.CurrentInteractable.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            if(enemy != null)
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

    public override void ResetInteraction()
    {
        enemy = null;
    }
}
