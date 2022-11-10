using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckManager : MonoBehaviour
{
    [SerializeField] private TriggerCheck boxFront = null;
    [SerializeField] private TriggerCheck boxLeft = null;
    [SerializeField] private TriggerCheck boxRight = null;
    [SerializeField] private TriggerCheck boxBack = null;
    private List<TriggerCheck> allChecks = new List<TriggerCheck>();

    public TriggerCheck BoxFront { get => boxFront; }
    public TriggerCheck BoxLeft { get => boxLeft; }
    public TriggerCheck BoxRight { get => boxRight; }
    public TriggerCheck BoxBack { get => boxBack; }
    public List<TriggerCheck> AllChecks { get => allChecks; }

    private void Start()
    {
        FillList();
    }

    private void FillList()
    {
        TriggerCheck[] children = GetComponentsInChildren<TriggerCheck>();

        foreach (TriggerCheck check in children)
        {
            allChecks.Add(check);
        }
    }
}
