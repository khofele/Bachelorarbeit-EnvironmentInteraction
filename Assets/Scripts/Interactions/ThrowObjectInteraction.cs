using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectInteraction : Interaction
{
    private CharController charController = null;
    private IKController iKController = null;

    public override void Start()
    {
        base.Start();
        charController = GetComponentInParent<CharController>();
        iKController = GetComponentInParent<IKController>();
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Debug.Log("Execute ThrowObjectInteraction");
        //Throw(); ist AnimationEvent
        // TODO Animation triggern, bisher in IKController
        interactionManager.CurrentInteraction = this;
    }

    public void Throw()
    {
        if (charController.CurrentInteractable != null)
        {
            charController.CurrentInteractable.transform.SetParent(iKController.Environment.transform, true);

            // Throw
            charController.Animator.SetLookAtWeight(10);
            Rigidbody rigidBody = charController.CurrentInteractable.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            rigidBody.AddForce(Camera.main.transform.forward * 20 + transform.up * 10, ForceMode.Impulse);
            // TODO Objekt in Richtung von Gegner werfen --> statt Camera.main.transform.forward anderen Gegenstand anpeilen

            charController.Animator.SetBool("isInteracting", false);
            iKController.IsIkActive = false;
        }
    }
}
