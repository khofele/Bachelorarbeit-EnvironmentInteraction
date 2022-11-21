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
        SnapToEnemy();
    }

    private void Update()
    {
        if (interactionManager.IsCharSnappingToEnemy == true && outcomeManager.CurrentOutcome == this)
        {
            SnapToEnemy();
        }
    }

    private Enemy GetCurrentEnemy()
    {
        return (Enemy)interactableManager.CurrentInteractable;
    }

    private void SnapToEnemy()
    {
        Vector3 position = currentEnemy.StompCollider.gameObject.GetComponent<Collider>().ClosestPoint(charController.transform.position);

        interactionManager.IsCharSnappingToEnemy = true;

        if(Vector3.Distance(charController.transform.position, position) < 0.5f)
        {
            animationManager.ExecuteStomp();

            interactionManager.IsCharSnappingToEnemy = false;

            ResetOutcome();
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, position, 3 * Time.deltaTime);
        }
    }
}
