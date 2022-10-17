using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Interactable : MonoBehaviour
{
    public virtual void Start()
    {
        Validate();
    }

    public virtual void Validate()
    {
        if(GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>().isTrigger = true;
        }
        else if(GetComponent<BoxCollider>().isTrigger == false)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
