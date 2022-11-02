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
    }

    protected override void Validate()
    {
        base.Validate();

        GetJumpCollider();
        CheckCubeSize();
    }

    private void GetJumpCollider()
    {
        Collider[] children = GetComponentsInChildren<Collider>();

        foreach (Collider child in children)
        {
            if (child.CompareTag("jumpCollider"))
            {
                jumpCollider = child;
            }
        }
    }

    private void CheckCubeSize()
    {
        if(GetComponent<BoxCollider>() != null)
        {
            if(transform.localScale.y < 0.6f || transform.localScale.y > 1.2f || transform.localScale.x > 1.5f)
            {
                Debug.LogWarning("The character might have some difficulties with jumping over the obstacle! Try smaller scales!");
            }
        }
        else
        {
            Debug.LogWarning("The character might have some difficulties with jumping over the obstacle!");
        }
    }
}
