using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnFloorCondition : Condition
{
    private Enemy enemy = null;
    private bool isEnemyOnFloor = false;

    public override bool CheckCondition()
    {
        return isEnemyOnFloor;
    }

    private void Update()
    {
        if (interactableManager.CurrentInteractable != null)
        {
            if (interactableManager.CurrentInteractable.GetType() == typeof(Enemy))
            {
                enemy = (Enemy)interactableManager.CurrentInteractable;

                if (enemy.IsOnFloor == false)
                {
                    isEnemyOnFloor = false;
                }
                else
                {
                    isEnemyOnFloor = true;
                }
            }
        }
    }
}
