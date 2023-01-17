using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEnemyOutcome : FightOutcome
{
    [SerializeField] private Transform grabHandle = null;

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);

        ResetOutcome();
        animationManager.ExecuteIdle();
        animationManager.DisableArmsLayer();
    }

    protected override void ReduceEnemyHealth()
    {
        currentEnemy.Health = 0f;
    }

    protected override void SnapToTarget()
    {
        target = GetCurrentTarget();

        Vector3 position = target.GetComponent<Collider>().ClosestPoint(charController.transform.position);

        interactionManager.IsCharSnappingToEnemy = true;

        if (Vector3.Distance(charController.transform.position, position) < 1f)
        {
            animationManager.ExecutePush();
            animationManager.EnableArmsLayer();
            interactionManager.IsCharSnappingToEnemy = false;
            DropEnemy();

            StartCoroutine(WaitAndReset());
            gameObject.GetComponentInParent<FistFightInteraction>().ResetInteraction();
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
