using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private Interactable currentInteractable = null;

    public Interactable CurrentInteractable { get => currentInteractable; set => currentInteractable = value; }
}
