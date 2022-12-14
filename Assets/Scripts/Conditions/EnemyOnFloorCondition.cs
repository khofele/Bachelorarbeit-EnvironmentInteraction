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
        if (interactableManager.CurrentInteractableParent != null)
        {
            if (interactableManager.CurrentInteractableParent.GetComponentInChildren<Enemy>() != null)
            {
                enemy = (Enemy)interactableManager.CurrentInteractableParent.GetComponentInChildren<Enemy>();
                
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
