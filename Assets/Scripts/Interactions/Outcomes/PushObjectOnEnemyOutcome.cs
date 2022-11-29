using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectOnEnemyOutcome : FightOutcome
{
    [SerializeField] private Transform grabHandle = null;
    private PushableTarget pushTarget = null;

    protected override void SnapToTarget()
    {
        pushTarget = (PushableTarget)GetCurrentTarget();

        Transform closestGrabHandle = GetClosestGrabHandle();

        Vector3 snapPosition = new Vector3(closestGrabHandle.position.x, charController.transform.position.y, closestGrabHandle.position.z);

        interactionManager.IsCharSnappingToEnemy = true;

        if (Vector3.Distance(charController.transform.position, snapPosition) < 0.5f)
        {
            // TODO KARO Animation ausführen --> TICKET: Größere Gegenstände schubsen
            interactionManager.IsCharSnappingToEnemy = false;
            DropTargetObject();

            // TODO KARO evtl. von PushEnemy copy paste StartCoroutine(WaitAndReset()); --> TICKET: Größere Gegenstände schubsen
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, snapPosition, 3 * Time.deltaTime);
            GrabTargetObject();
        }
    }

    private void GrabTargetObject()
    {
        pushTarget.transform.SetParent(grabHandle);
    }

    private void DropTargetObject()
    {
        pushTarget.transform.SetParent(null);
    }

    private Transform GetClosestGrabHandle()
    {
        float shortestDistance = Mathf.Infinity;
        Transform closestGrabHandle = null;

        foreach (Transform transform in pushTarget.GrabHandles)
        {
            float distance = Vector3.Distance(charController.transform.position, transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                closestGrabHandle = transform;
            }
        }

        return closestGrabHandle;
    }
}
