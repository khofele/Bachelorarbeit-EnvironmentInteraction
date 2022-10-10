using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{
    private Transform grabHandle = null;

    public Transform GrabHandle { get => grabHandle; }
}
