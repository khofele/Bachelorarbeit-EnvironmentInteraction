using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : Interactable
{
    private Transform touchHandle = null;
    private bool isTriggered = false;

    public Transform TouchHandle { get => touchHandle; }
    public bool IsTriggered { get => isTriggered; }

    public override void Start()
    {
        base.Start();

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach(Transform transform in children)
        {
            if(transform.CompareTag("TouchHandle"))
            {
                touchHandle = transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
