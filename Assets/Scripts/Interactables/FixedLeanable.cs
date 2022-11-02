using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedLeanable : Leanable
{
    private Transform resetTransformOne = null;
    private Transform resetTransformTwo = null;
    private Transform snapTransformOne = null;
    private Transform snapTransformTwo = null;
    private BoxCollider triggerOne = null;
    private BoxCollider triggerTwo = null;
    private int triggerCount = 0;

    public Transform ResetTransformOne { get => resetTransformOne; }
    public Transform ResetTransformTwo { get => resetTransformTwo; }
    public Transform SnapTransformOne { get => snapTransformOne; }
    public Transform SnapTransformTwo { get => snapTransformTwo; }
    public int TriggerCount { get => triggerCount; set => triggerCount = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharController>() != null)
        {
            triggerCount++;
        }
    }

    protected override void Validate()
    {
        base.Validate();
        // TODO Transforms (Snap und Reset) automatisiert an Gameobject anfügen + Tags   --> Validate-Ticket
        GetTransforms();
    }

    protected override void ModifyTrigger()
    {
        // Trigger 1 - existing trigger
        triggerOne = GetComponent<BoxCollider>();
        triggerOne.isTrigger = true;
        triggerOne.size = new Vector3(0.1f, 1f, 2.4f);
        triggerOne.center = new Vector3(0.45f, 0f, -0.7f);


        // Trigger 2 - new trigger
        BoxCollider[] boxColliders = GetComponents<BoxCollider>();

        // TODO schön machen  --> Validate-Ticket
        if (boxColliders.Length <= 1)
        {
            triggerTwo = gameObject.AddComponent<BoxCollider>();
            triggerTwo.isTrigger = true;
            triggerTwo.size = new Vector3(0.1f, 1f, 2.4f);
            triggerTwo.center = new Vector3(-0.45f, 0f, -0.7f);
        }
        else
        {
            triggerTwo = boxColliders[1];
            triggerTwo.isTrigger = true;
            triggerTwo.size = new Vector3(0.1f, 1f, 2.4f);
            triggerTwo.center = new Vector3(-0.45f, 0f, -0.7f);
        }

    }

    private void GetTransforms()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform transform in children)
        {
            if (transform.CompareTag("SnapTransformOne"))
            {
                snapTransformOne = transform;
            }
            else if (transform.CompareTag("SnapTransformTwo"))
            {
                snapTransformTwo = transform;
            }
            else if (transform.CompareTag("ResetTransformOne"))
            {
                resetTransformOne = transform;
            }
            else if (transform.CompareTag("ResetTransformTwo"))
            {
                resetTransformTwo = transform;
            }
        }
    }
}
