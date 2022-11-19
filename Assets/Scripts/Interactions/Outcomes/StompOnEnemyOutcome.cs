using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompOnEnemyOutcome : Outcome
{
    private Enemy currentEnemy = null;

    public override void ExecuteOutcome()
    {
        base.ExecuteOutcome();

        currentEnemy = GetCurrentEnemy();
        currentEnemy.Health = 0f;

        animationManager.ExecuteStomp();
    }

    private Enemy GetCurrentEnemy()
    {
        return (Enemy)interactableManager.CurrentInteractable;
    }
}
