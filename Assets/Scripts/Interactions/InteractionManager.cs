using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private InteractionEnum lastInteraction;
    
    public InteractionEnum LastInteraction { get => lastInteraction; set => lastInteraction = value; }
}
