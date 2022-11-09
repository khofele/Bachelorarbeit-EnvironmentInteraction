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
        GetTransforms();
    }

    protected override void ModifyTrigger()
    {
        // Trigger 1
        if (GetComponent<BoxCollider>() != null)
        {
            triggerOne = GetComponent<BoxCollider>();
            triggerOne.isTrigger = true;
            triggerOne.size = new Vector3(0.01f, 1f, 2.4f);
            triggerOne.center = new Vector3(0.5f, 0f, -0.7f);
        }
        else
        {
            triggerOne = gameObject.AddComponent<BoxCollider>();
            triggerOne.isTrigger = true;
            triggerOne.size = new Vector3(0.01f, 1f, 2.4f);
            triggerOne.center = new Vector3(0.5f, 0f, -0.7f);
        }

        // Trigger 2
        BoxCollider[] boxColliders = GetComponents<BoxCollider>();

        if (boxColliders.Length <= 1)
        {
            triggerTwo = gameObject.AddComponent<BoxCollider>();
            triggerTwo.isTrigger = true;
            triggerTwo.size = new Vector3(0.1f, 1f, 2.4f);
            triggerTwo.center = new Vector3(-0.45f, 0f, -0.7f);
        }
        else
        {
            foreach(BoxCollider boxCollider in boxColliders)
            {
                if (boxCollider != triggerOne)
                {
                    triggerTwo = boxCollider;
                    triggerTwo.isTrigger = true;
                    triggerTwo.size = new Vector3(0.1f, 1f, 2.4f);
                    triggerTwo.center = new Vector3(-0.45f, 0f, -0.7f);
                }
            }
        }

        if (triggerOne == null || triggerTwo == null)
        {
            Debug.LogError("Trigger not found!");
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
