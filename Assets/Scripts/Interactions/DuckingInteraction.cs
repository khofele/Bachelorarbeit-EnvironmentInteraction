using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckingInteraction : WalkThroughInteraction
{
    protected override void ResetInteraction()
    {
        //ResetCharacterCollider(); 
        //animationManager.StopCrouchAnimation();
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Duckable);
    }

    private void Duck()
    {
        ModifyCharacterCollider();
        animationManager.ExecuteCrouchAnimation(charController.ZAxis, charController.XAxis);
    }

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Duck();
    }

    private void ModifyCharacterCollider()
    {
        charController.GetComponent<CharacterController>().height = 1f;
        charController.GetComponent<CharacterController>().center = new Vector3(0, 0.5f, 0);
    }

    private void ResetCharacterCollider()
    {
        charController.GetComponent<CharacterController>().height = 2f;
        charController.GetComponent<CharacterController>().center = new Vector3(0, 1f, 0);
    }

    protected override void SetCurrentInteraction()
    {
        currentInteractable = (Duckable)interactableManager.CurrentInteractable;
    }

    protected override void ResetValues()
    {
        ResetCharacterCollider(); 
        animationManager.StopCrouchAnimation();
    }
}
