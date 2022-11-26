using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Climbable : Interactable
{
    [SerializeField] private Transform topTransform = null;
    [SerializeField] private Transform climbDownTransform = null;
    private TopClimbingTriggerCheck topClimbingTriggerCheck = null;
    private int triggerCount = 0;

    // DEBUG
    private List<ClimbingStone> climbingStones = new List<ClimbingStone>();
    public List<ClimbingStone> ClimbingStones { get => climbingStones; }
    // DEBUG

    public int TriggerCount { get => triggerCount; set => triggerCount = value; }
    public TopClimbingTriggerCheck TopClimbingTriggerCheck { get => topClimbingTriggerCheck; }
    public Transform TopTransform { get => topTransform; }
    public Transform ClimbDownTransform { get => climbDownTransform; }


    // TODO KARO Trigger richtig erkennen zu zuweisen evtl.  --> TICKET: KLETTERN OPTIMIERUNGEN
    // TODO KARO Validate-Methode  --> TICKET: KLETTERN OPTIMIERUNGEN

    protected override void Start()
    {
        base.Start();

        FillList();

        GetComponent<Collider>().isTrigger = false; 

        topClimbingTriggerCheck = GetComponentInChildren<TopClimbingTriggerCheck>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            triggerCount++;
        }
    }

    private void FillList()
    {
        climbingStones = GetComponentsInChildren<ClimbingStone>().ToList();
    }
}
