using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEnemyOnObjectOutcome : FightOutcome
{
    [SerializeField] private Transform grabHandle = null;

    protected override void ReduceEnemyHealth()
    {
        currentEnemy.Health = 0f;
    }

    protected override void SnapToTarget()
    {
        target = (FightTarget)GetCurrentTarget();

        Vector3 position = target.GetComponent<Collider>().ClosestPoint(charController.transform.position);

        interactionManager.IsCharSnappingToEnemy = true;

        if(Vector3.Distance(charController.transform.position, position) < 1f)
        {
            animationManager.ExecuteCrossPunchRight();
            DropEnemy();
            gameObject.GetComponentInParent<FistFightInteraction>().ResetInteraction();

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

    public override void ResetOutcome()
    {
        GetComponent<RandomCondition>().IsExecuted = false;
        base.ResetOutcome();
    }
}
