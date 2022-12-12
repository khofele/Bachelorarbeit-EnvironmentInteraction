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

        if (snapTransformOne == null || snapTransformTwo == null)
        {
            Debug.LogWarning("Snaptransform is missing!");
        }

        if (resetTransformOne == null || resetTransformTwo == null)
        {
            Debug.LogWarning("Resettransform is missing!");
        }
    }
}
