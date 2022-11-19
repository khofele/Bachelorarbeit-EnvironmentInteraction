using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAliveCondition : Condition
{
    private Enemy enemy = null;
    private bool isEnemyAlive = true;

    public override bool CheckCondition()
    {
        return isEnemyAlive;
    }

    private void Update()
    {
        if(interactableManager.CurrentInteractable != null)
        {
            if(interactableManager.CurrentInteractable.GetType() == typeof(Enemy))
            {
                enemy = (Enemy)interactableManager.CurrentInteractable;
                
                if(enemy.IsDead == false)
                {
                    isEnemyAlive = true;
                }
                else
                {
                    isEnemyAlive = false;
                }
            }
        }
    }
}
