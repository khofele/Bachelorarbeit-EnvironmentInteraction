using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : Interactable
{
    private Collider jumpCollider = null;

    public Collider JumpCollider { get => jumpCollider; }

    protected override void Start()
    {
        base.Start();
        GetComponent<Rigidbody>().isKinematic = true;
        jumpCollider = transform.GetChild(0).GetComponent<Collider>(); // --> Validate-Ticket
    }

    // TODO Validate, dass Höhe und Breite stimmt! --> Validate-Ticket
}
