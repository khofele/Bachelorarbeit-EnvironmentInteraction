using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class TargetObject : MonoBehaviour
{
    private Rigidbody rigidBody = null;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("TargetObjects");
    }
}
