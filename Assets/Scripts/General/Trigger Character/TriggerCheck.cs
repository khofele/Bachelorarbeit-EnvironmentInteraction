using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private bool isTriggered = false;
    public bool IsTriggered { get => isTriggered; set => isTriggered = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
