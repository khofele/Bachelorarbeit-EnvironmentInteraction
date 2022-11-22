using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckClimbable : MonoBehaviour
{
    private bool isTopReached = false;

    public bool IsTopReached { get => isTopReached; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isTopReached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isTopReached = false;
        }
    }
}
