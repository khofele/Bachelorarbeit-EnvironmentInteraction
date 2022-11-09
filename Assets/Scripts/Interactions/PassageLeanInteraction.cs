using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageLeanInteraction : FixedLeanInteraction
{
    protected override void SetLeanBool(bool value)
    {
        interactionManager.IsLeaningInPassage = value;
    }

    protected override bool CheckLeaningBool()
    {
        return interactionManager.IsLeaningInPassage;
    }

    protected override void SetCurrentLeanable()
    {
        currentLeanable = (PassageLeanable)interactableManager.CurrentInteractable;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(PassageLeanable);
    }

    protected override void DetectObject()
    {
        Ray rayFront = new Ray(charController.transform.position, charController.transform.forward);
        Ray rayBack = new Ray(charController.transform.position, -charController.transform.forward);
        Ray rayRight = new Ray(charController.transform.position, charController.transform.right);
        Ray rayLeft = new Ray(charController.transform.position, -charController.transform.right);

        RaycastHit hit;

        if ((Physics.Raycast(rayFront, out hit, 0.5f, -LayerMask.NameToLayer("Child"))) || (Physics.Raycast(rayBack, out hit, 0.5f, -LayerMask.NameToLayer("Child"))) || (Physics.Raycast(rayRight, out hit, 0.5f, -LayerMask.NameToLayer("Child"))) || (Physics.Raycast(rayLeft, out hit, 0.5f, -LayerMask.NameToLayer("Child"))))
        {
            if (hit.transform.gameObject.GetComponent<FixedLeanable>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
        }
    }
}
