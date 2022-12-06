using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private Interactable currentInteractable = null;
    private MultipleInteractionsInteractable currentMultipleInteractable = null;

    public Interactable CurrentInteractable { get => currentInteractable; set => currentInteractable = value; }
    public MultipleInteractionsInteractable CurrentMultipleInteractable { get => currentMultipleInteractable; set => currentMultipleInteractable = value; }
}
