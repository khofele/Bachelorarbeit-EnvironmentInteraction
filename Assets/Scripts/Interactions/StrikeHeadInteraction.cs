using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeHeadInteraction : Interaction
{
    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Enemy);
    }
}
