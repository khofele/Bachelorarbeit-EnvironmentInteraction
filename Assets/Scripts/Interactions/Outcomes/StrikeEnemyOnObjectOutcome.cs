using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEnemyOnObjectOutcome : Outcome
{
    [SerializeField] private Transform grabHandle = null;
    private InteractableManager interactableManager = null;
    private Enemy currentEnemy = null;
    private TargetObject target = null;

    public TargetObject Target { get => target; }
    public Enemy CurrentEnemy { get => currentEnemy; }

    protected override void Start()
    {
        base.Start();

        interactableManager = FindObjectOfType<InteractableManager>();
    }

    public override void ExecuteOutcome()
    {
        base.ExecuteOutcome();

        currentEnemy = GetCurrentEnemy();
        currentEnemy.Health = 0f;
        MoveToTarget();
        
        ResetOutcome();
    }

    private void Update()
    {
        if(interactionManager.IsFightSnapping == true)
        {
            SnapToTarget();
        }
    }

    public override void ResetOutcome()
    {
        GetComponent<RandomCondition>().IsExecuted = false;
        outcomeManager.CurrentOutcome = null;
        iKController.IsIkActive = false;
    }

    private Enemy GetCurrentEnemy()
    {
        return (Enemy)interactableManager.CurrentInteractable;
    }

    private void MoveToTarget()
    {
        target = GetCurrentTarget();
        SnapToTarget();
    }

    private TargetObject GetCurrentTarget()
    {
        return GetComponent<ObjectNearbyCondition>().Target;
    }

    private void SnapToTarget()
    {
        Vector3 position = target.GetComponent<Collider>().ClosestPoint(charController.transform.position);

        interactionManager.IsFightSnapping = true;

        if(Vector3.Distance(charController.transform.position, position) < 1f)
        {
            animationManager.ExecuteCrossPunchRight();
            DropEnemy();
            
            interactionManager.IsFightSnapping = false;
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
