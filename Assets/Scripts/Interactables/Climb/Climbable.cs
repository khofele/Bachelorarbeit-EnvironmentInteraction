using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactable
{
    [SerializeField] private Transform topTransform = null;
    private TriggerCheckClimbable triggerCheck = null;
    private int triggerCount = 0;

    public int TriggerCount { get => triggerCount; }
    public TriggerCheckClimbable TriggerCheck { get => triggerCheck; }
    public Transform TopTransform { get => topTransform; }

    // TODO KARO Trigger richtig erkennen zu zuweisen evtl.
    // TODO KARO Validate-Methode

    protected override void Start()
    {
        base.Start();

        triggerCheck = GetComponentInChildren<TriggerCheckClimbable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            triggerCount++;
        }
    }
}
