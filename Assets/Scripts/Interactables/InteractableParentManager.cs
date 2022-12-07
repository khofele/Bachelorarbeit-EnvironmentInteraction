using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractableParentManager : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();

    public List<Interactable> Interactables { get => interactables; }

    private void Start()
    {
        FillInteractablesList();

        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void FillInteractablesList()
    {
        interactables = GetComponentsInChildren<Interactable>().ToList();
    }
}
