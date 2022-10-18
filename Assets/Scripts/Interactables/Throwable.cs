using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{
    private Transform grabHandle = null;

    public override void Start()
    {
        base.Start();
    }

    public Transform GetGrabHandle()
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
