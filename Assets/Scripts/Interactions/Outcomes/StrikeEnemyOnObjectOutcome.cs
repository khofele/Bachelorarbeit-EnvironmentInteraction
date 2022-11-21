using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEnemyOnObjectOutcome : Outcome
{
    [SerializeField] private Transform grabHandle = null;
    private Enemy currentEnemy = null;
    private TargetObject target = null;

    public TargetObject Target { get => target; }
    public Enemy CurrentEnemy { get => currentEnemy; }

    public override void ExecuteOutcome()
    {
        base.ExecuteOutcome();

        currentEnemy = GetCurrentEnemy();
        currentEnemy.Health = 0f;
        SnapToTarget();
    }

    private void Update()
    {
        if(interactionManager.IsCharSnappingToEnemy == true && outcomeManager.CurrentOutcome == this)
        {
            SnapToTarget();
        }
    }

    public override void ResetOutcome()
    {
        GetComponent<RandomCondition>().IsExecuted = false;
        base.ResetOutcome();
    }

    private Enemy GetCurrentEnemy()
    {
        return (Enemy)interactableManager.CurrentInteractable;
    }

    private TargetObject GetCurrentTarget()
    {
        return GetComponent<ObjectNearbyCondition>().Target;
    }

    private void SnapToTarget()
    {
        target = GetCurrentTarget();

        Vector3 position = target.GetComponent<Collider>().ClosestPoint(charController.transform.position);

        interactionManager.IsCharSnappingToEnemy = true;

        if(Vector3.Distance(charController.transform.position, position) < 1f)
        {
            animationManager.ExecuteCrossPunchRight();
            DropEnemy();
            ResetOutcome();

            interactionManager.IsCharSnappingToEnemy = false;
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, position, 3 * Time.deltaTime);
            GrabEnemy();
        }
    }

    public void GrabEnemy()
    {
        currentEnemy.transform.SetParent(grabHandle);
    }

    public void DropEnemy()
    {
        currentEnemy.transform.SetParent(null);
    }
}
