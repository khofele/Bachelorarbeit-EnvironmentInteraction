using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject = null;
    private void Update()
    {
        float rotation = -(cameraObject.transform.rotation.x);
        transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }
}
