using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractableTriggerProperty : MonoBehaviour
{
    [HideInInspector] [SerializeField] private bool isActivatedFromEverySide = true;
    [HideInInspector] [SerializeField] private bool isBoxFront = false;
    [HideInInspector] [SerializeField] private bool isBoxLeft = false;
    [HideInInspector] [SerializeField] private bool isBoxRight = false;
    [HideInInspector] [SerializeField] private bool isBoxBack = false;
    private CharController charController = null;
    private List<TriggerCheck> triggerChecks = new List<TriggerCheck>();

    public bool IsActivatedFromEverySide { get => isActivatedFromEverySide; }
    public List<TriggerCheck> TriggerChecks { get => triggerChecks; }
    public bool IsBoxFront { get => isBoxFront; }
    public bool IsBoxLeft { get => isBoxLeft; }
    public bool IsBoxRight { get => isBoxRight; }
    public bool IsBoxBack { get => isBoxBack; }

    private void Start()
    {
        charController = FindObjectOfType<CharController>();
        FillList();
    }

    private void FillList()
    {
        if (isBoxFront == true)
        {
            triggerChecks.Add(charController.TriggerCheckManager.BoxFront);
        }
        
        if (isBoxLeft == true)
        {
            triggerChecks.Add(charController.TriggerCheckManager.BoxLeft);
        }

        if (isBoxRight == true)
        {
            triggerChecks.Add(charController.TriggerCheckManager.BoxRight);
        }

        if (isBoxBack == true)
        {
            triggerChecks.Add(charController.TriggerCheckManager.BoxBack);
        }
    }
}
