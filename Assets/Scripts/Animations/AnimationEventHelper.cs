using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class AnimationEventHelper : MonoBehaviour
{
    private ThrowObjectInteraction throwObjectInteraction = null;
    private FullBodyBipedIK fullBodyIK = null;
    private StrikeEnemyOnObjectOutcome strikeEnemyOnObject = null;
    private PushEnemyOutcome pushEnemy = null;
    private FinalIKController finalIKController = null;
    private PushObjectOnEnemyOutcome pushObject = null;

    private void Start()
    {
        throwObjectInteraction = GetComponentInChildren<ThrowObjectInteraction>();
        fullBodyIK = GetComponent<FullBodyBipedIK>();
        strikeEnemyOnObject = GetComponentInChildren<StrikeEnemyOnObjectOutcome>();
        pushEnemy = GetComponentInChildren<PushEnemyOutcome>();
        finalIKController = fullBodyIK.gameObject.GetComponent<FinalIKController>();
        pushObject = GetComponentInChildren<PushObjectOnEnemyOutcome>();
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

    public void SetRightFootPositionWeightToMax()
    {
        fullBodyIK.solver.rightFootEffector.positionWeight = 1f;
    }    
    
    public void SetRightFootPositionWeightToMin()
    {
        fullBodyIK.solver.rightFootEffector.positionWeight = 0f;
    }

    public void ExecuteEnableRagdollPhysics()
    {
        if (strikeEnemyOnObject.CurrentEnemy != null)
        {
            strikeEnemyOnObject.CurrentEnemy.EnableRagdollPhysics();
        }

        if (pushEnemy.CurrentEnemy != null)
        {
            pushEnemy.CurrentEnemy.EnableRagdollPhysics();
        }
    } 
    
    public void ExecuteDisableRagdollPhysics()
    {
        if(strikeEnemyOnObject.CurrentEnemy!= null)
        {
            strikeEnemyOnObject.CurrentEnemy.DisableRagdollPhysics();
        }
    }

    public void ExecuteDropEnemy()
    {
        if (strikeEnemyOnObject.CurrentEnemy != null)
        {
            strikeEnemyOnObject.DropEnemy();
        }
    }

    public void EnableIK()
    {
        finalIKController.IsIkActive = true;
    }

    public void ExecuteDropObject()
    {
        pushObject.DropTargetObject();
    }
}
