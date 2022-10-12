using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanOnWallInteraction : Interaction
{
    private void OnTriggerEnter(Collider other)
    {
        
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        Debug.Log("Execute LeanOnWallInteraction");
    }

    private void LeanOnWall()
    {

    }

    public override void ResetInteraction()
    {

    }
}
