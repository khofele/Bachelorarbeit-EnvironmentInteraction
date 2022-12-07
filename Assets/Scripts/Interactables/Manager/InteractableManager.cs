using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private Interactable currentInteractable = null;
    private InteractableParentManager currentInteractableParent = null;

    public Interactable CurrentInteractable { get => currentInteractable; set => currentInteractable = value; }
    public InteractableParentManager CurrentInteractableParent { get => currentInteractableParent; set => currentInteractableParent = value; }
}
