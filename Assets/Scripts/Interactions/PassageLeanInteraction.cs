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

        // ignores children of interactables!
        if ((Physics.Raycast(rayFront, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayBack, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayRight, out hit, 1f, LayerMask.NameToLayer("Checkbox"))) || (Physics.Raycast(rayLeft, out hit, 1f, LayerMask.NameToLayer("Checkbox"))))
        {
            if (hit.transform.gameObject.GetComponent<InteractableParentManager>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
            else if (hit.transform.gameObject.GetComponent<FixedLeanable>() != null)
            {
                charController.transform.rotation = Quaternion.LookRotation(-hit.transform.forward);
            }

        }
    }
}
