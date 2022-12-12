using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PushableTarget : TargetObject
{
    private List<Transform> grabHandles = new List<Transform>();

    public List<Transform> GrabHandles { get => grabHandles; }

    protected override void Start()
    {
        base.Start();

        FillList();
    }

    private void FillList()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        grabHandles = children.ToList();

        // Parent has to be removed!
        grabHandles.Remove(grabHandles[0]);

        if (grabHandles.Count == 0)
        {
            Debug.LogWarning("No grabhandles found!");
        }
    }
}
