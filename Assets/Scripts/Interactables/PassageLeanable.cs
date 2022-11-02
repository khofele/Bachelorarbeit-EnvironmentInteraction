using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageLeanable : FixedLeanable
{
    private BoxCollider oppositeWallCollider = null;

    public BoxCollider OppositeWall { get => oppositeWallCollider; }

    protected override void Validate()
    {
        base.Validate();

        GetSnapCollider();
        GetOppositeWall();
    }

    private void GetOppositeWall()
    {
        BoxCollider[] children = transform.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider child in children)
        {
            if (child.CompareTag("OppositeWall"))
            {
                oppositeWallCollider = child;
            }
        }

        if (oppositeWallCollider == null)
        {
            Debug.LogError("No opposite wall found!");
        }
    }
}
