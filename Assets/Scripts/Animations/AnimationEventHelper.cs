using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class AnimationEventHelper : MonoBehaviour
{
    private ThrowObjectInteraction throwObjectInteraction = null;
    private FullBodyBipedIK fullBodyIK = null;

    private void Start()
    {
        throwObjectInteraction = GetComponentInChildren<ThrowObjectInteraction>();
        fullBodyIK = GetComponent<FullBodyBipedIK>();
    }

    public void ExecuteThrow()
    {
        throwObjectInteraction.Throw();
    }

    public void ExecuteEnableCollider()
    {
        throwObjectInteraction.EnableCollider();
    }

    public void ExecuteDisableCollider()
    {
        throwObjectInteraction.DisableCollider();   
    }

    public void SetRightHandPositionWeightToMax()
    {
        fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
    }    
    
    public void SetRightHandPositionWeightToMin()
    {
        fullBodyIK.solver.rightHandEffector.positionWeight = 0f;
    }

    public void SetLefttHandPositionWeightToMax()
    {
        fullBodyIK.solver.leftHandEffector.positionWeight = 1f;
    }

    public void SetLeftHandPositionWeightToMin()
    {
        fullBodyIK.solver.leftHandEffector.positionWeight = 0f;
    }
}
