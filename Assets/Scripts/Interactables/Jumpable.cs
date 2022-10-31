using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : Interactable
{
    protected override void Start()
    {
        base.Start();
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
