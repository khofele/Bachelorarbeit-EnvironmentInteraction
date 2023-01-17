using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompOnEnemyOutcome : FightOutcome
{
    protected override void ReduceEnemyHealth()
    {
        currentEnemy.Health = 0f;
    }

    protected override void SnapToTarget()
    {
        Vector3 position = currentEnemy.StompCollider.gameObject.GetComponent<Collider>().ClosestPoint(charController.transform.position);

        interactionManager.IsCharSnappingToEnemy = true;

        if (Vector3.Distance(charController.transform.position, position) < 0.5f)
        {
            animationManager.ExecuteStomp();

            interactionManager.IsCharSnappingToEnemy = false;

            //base.ResetOutcome();
            gameObject.GetComponentInParent<FistFightInteraction>().ResetInteraction();
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, position, 6 * Time.deltaTime);
        }
    }
}
