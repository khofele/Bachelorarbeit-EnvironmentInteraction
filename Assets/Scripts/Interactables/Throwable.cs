using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public Transform GetGrabHandle()
    {
        Transform parentTransform = transform;
        Transform grabHandle = null;

        foreach (Transform transform in parentTransform)
        {
            if (transform.CompareTag("grabHandle"))
            {
                grabHandle = transform;
            }
        }

        if (grabHandle == null)
        {
            Debug.LogError("No grabhandle found!");
            return null;
        }
        else
        {
            return grabHandle;
        }
    }
}
