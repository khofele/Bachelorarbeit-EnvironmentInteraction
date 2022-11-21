using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEnemyOutcome : Outcome
{
    [SerializeField] private Transform grabHandle = null;
    private Enemy currentEnemy = null;
    private TargetObject target = null;

    public Enemy CurrentEnemy { get => currentEnemy; }

    public override void ExecuteOutcome()
    {
        base.ExecuteOutcome();

        currentEnemy = GetCurrentEnemy();
        currentEnemy.Health = 0f;

        SnapToTarget();
    }

    private Enemy GetCurrentEnemy()
    {
        return (Enemy)interactableManager.CurrentInteractable;
    }

    private TargetObject GetCurrentTarget()
    {
        return GetComponent<ObjectNearbyCondition>().Target;
    }

    private void Update()
    {
        if (interactionManager.IsCharSnappingToEnemy == true && outcomeManager.CurrentOutcome == this)
        {
            SnapToTarget();
        }
    }

    private void SnapToTarget()
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

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);

        ResetOutcome();
        animationManager.ExecuteIdle();
    }
}
