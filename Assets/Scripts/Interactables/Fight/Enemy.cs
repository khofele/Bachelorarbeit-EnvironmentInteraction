using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    [SerializeField] private Collider stompCollider = null;
    private CapsuleCollider bodyCollider = null;
    private float health = 100f;
    private InteractionManager interactionManager = null;
    private bool isDead = false;
    private bool gotHit = false;
    private Rigidbody[] rigidbodies;
    private bool isOnFloor = false;

    public Collider StompCollider { get => stompCollider; }
    public CapsuleCollider BodyCollider { get => bodyCollider; }
    public float Health { get => health; set => health = value; }
    public bool IsDead { get => isDead; }
    public bool IsOnFloor { get => isOnFloor; }

    protected override void Start()
    {
        base.Start();

        interactionManager = FindObjectOfType<InteractionManager>();

        FillRigidbodiesArray();
        DisableRagdollPhysics();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Checkbox"), LayerMask.NameToLayer("Interactable"), true);
    }

    protected override void Validate()
    {
        base.Validate();

        bodyCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (interactionManager.CurrentInteraction != null)
        {
            if (interactionManager.CurrentInteraction.GetType() == typeof(FistFightInteraction) && interactionManager.CurrentInteraction.IsInteractionRunning == true && isDead == false && gotHit == false && interactableManager.CurrentInteractable == this) {
                
                MultipleOutcomesInteraction current = (MultipleOutcomesInteraction)interactionManager.CurrentInteraction;

                if(current != null)
                {
                    if (current.OutcomeManager.CurrentOutcome == null)
                    {
                        gotHit = true;
                        health -= 10f;

                        FallOnFloor();

                        if(health <= 0) 
                        {
                            StartCoroutine(new WaitForSecondsRealtime(1f));
                            EnableRagdollPhysics();
                        }
                    }
                }
            }

            if (interactionManager.CurrentInteraction.IsInteractionRunning == false)
            {
                gotHit = false;
            }
        }
        
        if (health <= 0f)
        {
            isDead = true;
            DisableCapsuleCollider();
        }
    }

    private void FillRigidbodiesArray()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void EnableRagdollPhysics()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }

    public void DisableRagdollPhysics()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
    }

    private void DisableCapsuleCollider()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Throwable>() != null)
        {
            health -= 100f;
            EnableRagdollPhysics();
        }
    }

    private void FallOnFloor()
    {
        int random = Random.Range(1, 11);

        if(random % 5 == 0)
        {
            EnableRagdollPhysics();
            isOnFloor = true;
        }
    }
}
