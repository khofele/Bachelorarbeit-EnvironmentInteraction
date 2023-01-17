using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Interactable
{
    [SerializeField] private Collider stompCollider = null;
    [SerializeField] private Transform pushHandleLeft = null;
    [SerializeField] private Transform pushHandleRight = null;
    private CapsuleCollider bodyCollider = null;
    private float health = 100f;
    private InteractionManager interactionManager = null;
    private bool isDead = false;
    private bool gotHit = false;
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private bool isOnFloor = false;
    private Renderer enemyRenderer = null;

    public Collider StompCollider { get => stompCollider; }
    public CapsuleCollider BodyCollider { get => bodyCollider; }
    public float Health { get => health; set => health = value; }
    public bool IsDead { get => isDead; }
    public bool IsOnFloor { get => isOnFloor; }
    public Transform PushHandleLeft { get => pushHandleLeft; }
    public Transform PushHandleRight { get => pushHandleRight; }

    private void Update()
    {
        if (interactionManager.CurrentInteraction != null)
        {
            if (interactionManager.CurrentInteraction.GetType() == typeof(FistFightInteraction) && interactionManager.CurrentInteraction.IsInteractionRunning == true && isDead == false && gotHit == false && interactableManager.CurrentInteractable == this) {
                
                MultipleOutcomesInteraction current = (MultipleOutcomesInteraction)interactionManager.CurrentInteraction;

                if (current != null)
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
            enemyRenderer.material.SetColor("_Color", Color.grey);
            DisableCapsuleCollider();
        }
    }

    private void FillRigidbodiesArray()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        rigidbodies.Remove(rigidbodies[0]);
    }

    private void DisableCapsuleCollider()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.GetComponent<Throwable>() != null) 
        {
            health -= 100f;
            EnableRagdollPhysics();
        }
        
        if (other.gameObject.GetComponent<PushableTarget>() != null && interactionManager.CurrentInteraction != null)
        {
            if (interactionManager.CurrentInteraction.gameObject.GetComponent<OutcomeManager>() != null)
            {
                if (interactionManager.CurrentInteraction.gameObject.GetComponent<OutcomeManager>().CurrentOutcome != null)
                {
                    if (interactionManager.CurrentInteraction.gameObject.GetComponent<OutcomeManager>().CurrentOutcome.GetType() == typeof(PushObjectOnEnemyOutcome))
                    {
                        EnableRagdollPhysics();
                    }
                }
            }
        }
        
        if (other.gameObject.GetComponent<PushableTarget>() != null && interactionManager.LastInteraction != null)
        {
            if (interactionManager.LastInteraction.gameObject.GetComponent<OutcomeManager>() != null)
            {
                if (interactionManager.LastInteraction.gameObject.GetComponent<OutcomeManager>().LastOutcome != null)
                {
                    if (interactionManager.LastInteraction.gameObject.GetComponent<OutcomeManager>().LastOutcome.GetType() == typeof(PushObjectOnEnemyOutcome))
                    {
                        EnableRagdollPhysics();
                    }
                }
            }
        }
        
    }

    private void FallOnFloor()
    {
        int random = Random.Range(1, 11);

        if (random % 5 == 0)
        {
            EnableRagdollPhysics();
            isOnFloor = true;
        }
    }

    private IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(1.75f);
        DisableRagdollPhysics();
    }

    protected override void Start()
    {
        base.Start();

        interactionManager = FindObjectOfType<InteractionManager>();

        FillRigidbodiesArray();
        DisableRagdollPhysics();

        enemyRenderer = gameObject.GetComponentInChildren<Renderer>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Checkbox"), LayerMask.NameToLayer("Interactable"), true);
    }

    protected override void Validate()
    {
        base.Validate();

        bodyCollider = GetComponent<CapsuleCollider>();
    }

    public void EnableRagdollPhysics()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }

        isOnFloor = true;

        StartCoroutine(WaitAndDisable());
    }

    public void DisableRagdollPhysics()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
    }
}
