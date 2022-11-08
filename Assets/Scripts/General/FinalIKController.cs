using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIKController : MonoBehaviour
{
    [Header("Right Hand Transforms")]
    [SerializeField] private Transform rightHandGrabHandle = null;
    [SerializeField] private Transform rightHandLookTarget = null;
    [SerializeField] private Transform leanTargetRightHand = null;
    [SerializeField] private Transform passageLeanTargetRightHand = null;

    [Header("Left Hand Transforms")]
    [SerializeField] private Transform leftHandGrabHandle = null;
    [SerializeField] private Transform leftHandLookTarget = null;
    [SerializeField] private Transform leanTargetLeftHand = null;
    [SerializeField] private Transform passageLeanTargetLeftHand = null;

    // general
    private FullBodyBipedIK fullBodyIK = null;
    private LookAtIK lookAtIK = null;
    private bool isIkActive = false;
    private CharController charController = null;
    private InteractionManager interactionManager = null;
    private AnimationManager animationManager = null;
    private InteractableManager interactableManager = null;

    // throw
    private bool isThrowHandChosen = false;
    private Transform closestHand = null;
    private Transform grabHandleOfThrowable = null;

    public bool IsIkActive { get => isIkActive; set => isIkActive = value; }

    private void Start()
    {
        fullBodyIK = GetComponent<FullBodyBipedIK>();
        lookAtIK = GetComponent<LookAtIK>();
        charController = GetComponent<CharController>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        animationManager = GetComponent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
    }

    private void OnAnimatorIK()
    {
        if (fullBodyIK == true)
        {
            if (isIkActive == true)
            {
                if (interactionManager.CurrentInteraction != null)
                {
                    GameObject currentInteractionGameObject = interactionManager.CurrentInteraction.gameObject;

                    // Throw
                    if (currentInteractionGameObject.GetComponent<ThrowObjectInteraction>() != null)
                    {
                        ThrowObjectIK();
                    }
                    //// Lean: Passage
                    //else if (currentInteractionGameObject.GetComponent<PassageLeanInteraction>() != null)
                    //{
                    //    PassageLeanIK();
                    //}
                    // Lean: Crouch, on Edge and Stand
                    else if (currentInteractionGameObject.GetComponent<LeanInteraction>() != null)
                    {
                        LeanIK();
                    }
                    //// Touch door
                    //else if (currentInteractionGameObject.GetComponent<TouchObjectInteraction>() != null)
                    //{
                    //    TouchIK();
                    //}
                    // Jump over
                    else if (currentInteractionGameObject.GetComponent<JumpOverObstacleInteraction>() != null)
                    {
                        JumpIK();
                    }
                }
            }
            else
            {
                Reset();
                isThrowHandChosen = false;
        }
    }
    }

    private void ThrowObjectIK()
    {
        Throwable currentInteractable = (Throwable)interactableManager.CurrentInteractable;

        if (currentInteractable != null)
        {
            grabHandleOfThrowable = currentInteractable.GetGrabHandle();

            if (grabHandleOfThrowable.gameObject != null)
            {
                if (isThrowHandChosen == false)
                {
                    isThrowHandChosen = true;
                    closestHand = GetClosestHand();
                }

                if (closestHand == rightHandGrabHandle)
                {
                    // TODO Karo Ball richtig in Hand nehmen --> siehe Videotutorial --> Ticket: Wurfoptimierung 2
                    // TODO Karo evtl. Bodyweight --> Ticket: Wurfoptimierung 2
                    fullBodyIK.solver.rightHandEffector.position = grabHandleOfThrowable.position;
                    fullBodyIK.solver.rightHandEffector.rotation = grabHandleOfThrowable.rotation;
                    fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
                    fullBodyIK.solver.rightHandEffector.rotationWeight = 1f;
                }
                else
                {
                    // TODO Karo Ball richtig in Hand nehmen --> siehe Videotutorial --> Ticket: Wurfoptimierung 2
                    // TODO Karo evtl. Bodyweight --> Ticket: Wurfoptimierung 2
                    fullBodyIK.solver.leftHandEffector.position = grabHandleOfThrowable.position;
                    fullBodyIK.solver.leftHandEffector.rotation = grabHandleOfThrowable.rotation;
                    fullBodyIK.solver.leftHandEffector.positionWeight = 1f;
                    fullBodyIK.solver.leftHandEffector.rotationWeight = 1f;
                }

                currentInteractable.GetComponent<Rigidbody>().useGravity = false;
                currentInteractable.GetComponent<Rigidbody>().isKinematic = true;

                lookAtIK.solver.target = currentInteractable.transform;
                lookAtIK.solver.IKPositionWeight = 1f;

                currentInteractable.transform.position = closestHand.position;
                currentInteractable.transform.SetParent(closestHand);

                isIkActive = false;

                ExecuteThrowAnimation(closestHand);
            }
        }
    }

    private Transform GetClosestHand()
    {
        Throwable currentThrowable = (Throwable)interactableManager.CurrentInteractable;

        float distanceLeftHand = Vector3.Distance(leftHandGrabHandle.position, currentThrowable.transform.position);
        float distanceRightHand = Vector3.Distance(rightHandGrabHandle.position, currentThrowable.transform.position);

        if (distanceLeftHand < distanceRightHand)
        {
            return leftHandGrabHandle;
        }
        else
        {
            return rightHandGrabHandle;
        }
    }

    private void ExecuteThrowAnimation(Transform closestHand)
    {
        if (closestHand == leftHandGrabHandle)
        {
            animationManager.ExecuteThrowAnimation("isThrowingLeft");
        }
        else
        {
            animationManager.ExecuteThrowAnimation("isThrowingRight");
        }
    }

    private void JumpIK()
    {
        Jumpable currentInteractable = (Jumpable)interactableManager.CurrentInteractable;

        Collider jumpCollider = currentInteractable.JumpCollider;

        Vector3 closestPointRightHand = jumpCollider.ClosestPoint(rightHandGrabHandle.position);
        Vector3 closestPointLeftHand = jumpCollider.ClosestPoint(leftHandGrabHandle.position);

        fullBodyIK.solver.rightHandEffector.position = closestPointRightHand;
        fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
        fullBodyIK.solver.rightHandEffector.rotationWeight = 1f;

        fullBodyIK.solver.leftHandEffector.position = closestPointLeftHand;
        fullBodyIK.solver.leftHandEffector.positionWeight = 1f;
        fullBodyIK.solver.leftHandEffector.rotationWeight = 1f;
    }

    private void LeanIK()
    {
        Leanable currentInteractable = (Leanable)interactableManager.CurrentInteractable;

        // right hand
        Vector3 closestPointRightHand = currentInteractable.SnapCollider.ClosestPoint(leanTargetRightHand.position);
        fullBodyIK.solver.rightHandEffector.position = closestPointRightHand;
        fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
        fullBodyIK.solver.rightHandEffector.rotation = leanTargetRightHand.rotation;
        fullBodyIK.solver.rightHandEffector.rotationWeight = 1f;

        // left hand
        Vector3 closestPointLeftHand = currentInteractable.SnapCollider.ClosestPoint(leanTargetLeftHand.position);
        fullBodyIK.solver.leftHandEffector.position = closestPointLeftHand;
        fullBodyIK.solver.leftHandEffector.positionWeight = 1f;
        fullBodyIK.solver.leftHandEffector.rotation = leanTargetLeftHand.rotation;
        fullBodyIK.solver.leftHandEffector.rotationWeight = 1f; 

        // viewing direction
        FaceDirection();
    }

    private void FaceDirection()
    {
        if (charController.XAxis == 1)
        {
            lookAtIK.solver.target = leftHandLookTarget;
        }
        else if (charController.XAxis == -1)
        {
            lookAtIK.solver.target = rightHandLookTarget;
        }
        else
        {
            lookAtIK.solver.target = null;
            lookAtIK.solver.IKPositionWeight = 0f;
        }

        lookAtIK.solver.IKPositionWeight = 1f;
    }

    private void Reset()
    {
        fullBodyIK.solver.leftHandEffector.positionWeight = 0f;
        fullBodyIK.solver.leftHandEffector.rotationWeight = 0f;
        fullBodyIK.solver.rightHandEffector.positionWeight = 0f;
        fullBodyIK.solver.rightHandEffector.rotationWeight = 0f;
        lookAtIK.solver.target = null;
        lookAtIK.solver.IKPositionWeight = 0f;
        isIkActive = false;
    }
}
