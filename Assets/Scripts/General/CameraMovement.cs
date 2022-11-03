using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera = null;
    [SerializeField] private CinemachineVirtualCamera leanCamera = null;
    [SerializeField] private CinemachineVirtualCamera topDownCamera = null;
    private InteractionManager interactionManager = null;
    private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    private void Start()
    {
        interactionManager = GetComponentInChildren<InteractionManager>();

        virtualCameras.Add(leanCamera);
        virtualCameras.Add(topDownCamera);
    }

    private void Update()
    {
        if (interactionManager.IsLeaning == false)
        {
            freeLookCamera.enabled = true;
        }
        else if (interactionManager.IsLeaningInPassage == true)
        {
            EnableVirtualCamera(topDownCamera);
        }
        else
        {
            EnableVirtualCamera(leanCamera);
        }
    }

    private void EnableVirtualCamera(CinemachineVirtualCamera camera)
    {
        foreach(CinemachineVirtualCamera vCam in virtualCameras)
        {
            if (vCam == camera)
            {
                vCam.enabled = true;
            }
            else
            {
                vCam.enabled = false;
            }
        }
        freeLookCamera.enabled = false;
    }
}
