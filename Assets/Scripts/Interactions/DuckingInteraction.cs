using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckingInteraction : WalkThroughInteraction
{
    private void Duck()
    {
        isTriggeredByInterruptibleInteraction = true;
        ModifyCharacterCollider();
        animationManager.ExecuteCrouchAnimation(charController.ZAxis, charController.XAxis);
        animationManager.ExecuteDuckingAnimation();
        animationManager.EnableUpperBodyLayer();
    }

    private void ModifyCharacterCollider()
    {
        charController.GetComponent<CharacterController>().height = 1f;
        charController.GetComponent<CharacterController>().center = new Vector3(0f, 0.5f, 0f);
    }

    private void ResetCharacterCollider()
    {
        charController.GetComponent<CharacterController>().height = 2f;
        charController.GetComponent<CharacterController>().center = new Vector3(0f, 1f, 0f);
    }

    protected override void SetCurrentInteraction()
    {
        currentInteractable = (Duckable)interactableManager.CurrentInteractable;
    }



    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Duckable);
    }

    public override void ResetInteraction()
    {
        interactionManager.SetLastInteraction();
        ResetCharacterCollider(); 
        animationManager.StopCrouchAnimation();
        animationManager.StopDuckingAnimation();
        animationManager.DisableUpperBodyLayer();   
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Duck();
    }
}
