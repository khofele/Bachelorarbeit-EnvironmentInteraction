using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{
    private Transform grabHandle = null;

    public Transform GrabHandle { get => grabHandle; }

    private void Start()
    {
        grabHandle = GetGrabHandle();
        Debug.Log("lol " + grabHandle.name);
    }

    private Transform GetGrabHandle()
    {
        Transform parentTransform = transform;

        foreach(Transform transform in parentTransform)
        {
            if(transform.CompareTag("grabHandle"))
            {
                return transform;
            }
        }
        return null;
    }
}
