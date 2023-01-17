using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectOnEnemyOutcome : FightOutcome
{
    [SerializeField] private Transform grabHandle = null;
    private PushableTarget pushTarget = null;
    private Transform closestGrabHandle = null;

    public PushableTarget PushTarget { get => pushTarget; }
    public Transform ClosestGrabHandle { get => closestGrabHandle; }

    private void GrabTargetObject()
    {
        pushTarget.transform.SetParent(grabHandle);
        pushTarget.transform.localPosition = Vector3.zero;
    }

    private Transform GetClosestGrabHandle()
    {
        float shortestDistance = Mathf.Infinity;
        closestGrabHandle = null;

        foreach (Transform transform in pushTarget.GrabHandles)
        {
            float distance = Vector3.Distance(charController.transform.position, transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestGrabHandle = transform;
            }
        }
        return closestGrabHandle;
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2f);
        interactionManager.IsPushingObject = false;
    }

    protected override void ReduceEnemyHealth()
    {
        currentEnemy.Health -= 50f;
    }

    protected override void SnapToTarget()
    {
        pushTarget = (PushableTarget)GetCurrentTarget();

        Transform closestGrabHandle = GetClosestGrabHandle();

        Vector3 snapPosition = new Vector3(closestGrabHandle.position.x, charController.transform.position.y, closestGrabHandle.position.z);

        interactionManager.IsCharSnappingToEnemy = true;

        if (Vector3.Distance(charController.transform.position, snapPosition) < 0.2f)
        {
            GrabTargetObject();

            charController.transform.LookAt(currentEnemy.transform, gameObject.transform.up);
            animationManager.ExecutePushObject();
            interactionManager.IsCharSnappingToEnemy = false;
            StartCoroutine(WaitAndReset());
            gameObject.GetComponentInParent<FistFightInteraction>().ResetInteraction();
        }
        else
        {
            charController.transform.position = Vector3.MoveTowards(charController.transform.position, snapPosition, 3 * Time.deltaTime);
        }
    }

    public void DropTargetObject()
    {
        pushTarget.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        pushTarget.gameObject.GetComponent<Rigidbody>().AddForce(currentEnemy.transform.position * 4, ForceMode.Impulse);
        pushTarget.transform.SetParent(null);
    }

    public override void ExecuteOutcome()
    {
        base.ExecuteOutcome();
        iKController.IsIkActive = false;
        interactionManager.IsPushingObject = true;
    }

    public override void ResetOutcome()
    {
        base.ResetOutcome();

        StartCoroutine(WaitAndReset());
    }
}
