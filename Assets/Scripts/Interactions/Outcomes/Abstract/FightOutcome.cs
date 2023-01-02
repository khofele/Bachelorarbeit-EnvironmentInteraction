using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightOutcome : Outcome
{
    protected Enemy currentEnemy = null;
    protected TargetObject target = null;

    public Enemy CurrentEnemy { get => currentEnemy; }
    public TargetObject Target { get => target; }

    protected virtual Enemy GetCurrentEnemy()
    {
        return (Enemy)interactableManager.CurrentInteractable;
    }

    protected virtual TargetObject GetCurrentTarget()
    {
        return GetComponent<ObjectNearbyCondition>().Target;
    }

    protected virtual void Update()
    {
        if (interactionManager.IsCharSnappingToEnemy == true && outcomeManager.CurrentOutcome == this)
        {
            SnapToTarget();
        }
    }

    protected abstract void SnapToTarget();

    protected abstract void ReduceEnemyHealth();

    public override void ExecuteOutcome()
    {
        base.ExecuteOutcome();

        currentEnemy = GetCurrentEnemy();
        ReduceEnemyHealth();

        SnapToTarget();
    }
}
