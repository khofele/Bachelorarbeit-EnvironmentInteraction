using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompOnEnemyOutcome : FightOutcome
{
    protected override void SnapToTarget()
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
