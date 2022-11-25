using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopClimbingTriggerCheck : MonoBehaviour
{
    private bool isOnTopOfWall = false;

    public bool IsOnTopOfWall { get => isOnTopOfWall; set => isOnTopOfWall = value; }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isOnTopOfWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            isOnTopOfWall = false;
        }
    }
}
