using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutcomeManager : MonoBehaviour
{
    private List<Outcome> outcomes = new List<Outcome>();
    private Outcome currentOutcome = null;

    public List<Outcome> Outcomes { get => outcomes; }
    public Outcome CurrentOutcome { get => currentOutcome; set => currentOutcome = value; }
    
    public void FillOutcomesList()
    {
        Outcome[] allOutcomes = GetComponentsInChildren<Outcome>();

        foreach(Outcome outcome in allOutcomes)
        {
            outcomes.Add(outcome);
        }
    }

    public void ResetOutcomes()
    {
        foreach (Outcome outcome in outcomes)
        {
            outcome.ResetOutcome();
        }
    }
}
