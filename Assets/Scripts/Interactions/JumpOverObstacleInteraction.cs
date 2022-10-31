using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOverObstacleInteraction : Interaction
{
    protected override void ResetInteraction()
    {
        StartCoroutine(WaitAndReset());
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Jumpable);
    }

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Jump();
    }

    protected override void Update()
    {
        base.Update();

        if(interactionManager.IsJumping == true)
        {
            charController.transform.position += (new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized) * Time.deltaTime * 2;
        }
    }

    private void Jump()
    {
        animationManager.ExecuteJumpAnimation();
        interactionManager.IsJumping = true;
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);
        interactionManager.IsJumping = false;
        isInteractionRunning = false;
    }
}
