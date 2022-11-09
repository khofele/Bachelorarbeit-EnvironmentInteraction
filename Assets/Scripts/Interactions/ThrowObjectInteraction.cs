using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectInteraction : Interaction
{
    [SerializeField] private GameObject interactablesGameObject = null;
    private GameObject enemy = null;
    private SphereCollider sphereCollider = null;
    private List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private GameObject GetClosestEnemy()
    {
        float distance = 1000f;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            if (Vector3.Distance(charController.transform.position, enemy.transform.position) < distance)
            {
                distance = Vector3.Distance(charController.transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    protected override void Start()
    {
        base.Start();
        sphereCollider = GetComponent<SphereCollider>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Interactable"), LayerMask.NameToLayer("Checkbox"));
        sphereCollider.enabled = false;
    }

    protected override void Update()
    {
        if (CheckTrigger() == true)
        {
            if (CheckMatchingInteractable() == true)
            {
                if (CheckConditions() == true)
                {
                    ExecuteInteraction();
                    ResetInteraction();
                }
            }
        }
    }

    protected override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        //Throw(); ist AnimationEvent
    }

    public void Throw()
    {
        if (interactableManager.CurrentInteractable != null)
        {
            interactableManager.CurrentInteractable.transform.SetParent(interactablesGameObject.transform, true);

            Rigidbody rigidBody = interactableManager.CurrentInteractable.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            enemy = GetClosestEnemy();

            if (enemy != null)
            {
                Vector3 direction = enemy.transform.position - charController.transform.position;
                rigidBody.AddForce(direction.normalized * 15 + transform.forward * 0.5f, ForceMode.Impulse);
            }
            else
            {
                rigidBody.AddForce(Camera.main.transform.forward * 15 + transform.up, ForceMode.Impulse);
            }

            animationManager.StopThrowAnimation();
            finalIKController.IsIkActive = false;
            isInteractionRunning = false;
            enemies.Clear();
        }
    }

    public void EnableCollider()
    {
        sphereCollider.enabled = true;
    }

    public void DisableCollider()
    {
        sphereCollider.enabled = false;
    }

    protected override void ResetInteraction()
    {
        enemy = null;
    }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Throwable);
    }
}
