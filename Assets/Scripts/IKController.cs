using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    // TODO bessere Lösung für Hände finden?
    // right hand
    [SerializeField] private Transform rightHandGrabHandle = null;
    [SerializeField] private Transform rightHandWatchHandle = null;
    [SerializeField] private Transform leanRightHand = null;
    [SerializeField] private Transform passageLeanRightHand = null;

    // left hand
    [SerializeField] private Transform leftHandGrabHandle = null;
    [SerializeField] private Transform leftHandWatchHandle = null;
    [SerializeField] private Transform leanLeftHand = null;
    [SerializeField] private Transform passageLeanLeftHand = null;

    // general
    [SerializeField] private GameObject interactables = null;
    [SerializeField] private Transform watchHandle = null;
    private bool isIkActive = false;
    private Animator animator = null;
    private Transform grabHandle = null;
    private CharController charController = null;
    private InteractionManager interactionManager = null;
    private AnimationManager animationManager = null;
    private InteractableManager interactableManager = null;

    // lean
    private Vector3 closestPointRightHand = Vector3.zero;
    private Vector3 closestPointLeftHand = Vector3.zero;

    // touch
    private bool isLeftTouchHandleChosen = false;
    private bool isTouchHandleChosen = false;
    private Transform closestTouchHandleRight = null;
    private Transform closestTouchHandleLeft = null;

    public GameObject Interactables { get => interactables; }
    public bool IsIkActive { get => isIkActive; set => isIkActive = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharController>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        animationManager = GetComponent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
    }

    private void OnAnimatorIK()
    {
        if (animator == true)
        {
            if (isIkActive == true)
            {
                if(interactionManager.CurrentInteraction != null)
                {
                    GameObject currentInteractionGameObject = interactionManager.CurrentInteraction.gameObject;

                    // Throw
                    if (currentInteractionGameObject.GetComponent<ThrowObjectInteraction>() != null)
                    {
                        ThrowObjectIK();
                    }
                    // Lean: Passage
                    else if(currentInteractionGameObject.GetComponent<PassageLeanInteraction>() != null)
                    {
                        PassageLeanIK();
                    }
                    // Lean: Crouch, on Edge and Cover
                    else if(currentInteractionGameObject.GetComponent<LeanInteraction>() != null)
                    {
                        LeanIK();
                    }
                    // Touch door
                    else if(currentInteractionGameObject.GetComponent<TouchObjectInteraction>() != null)
                    {
                        TouchIK();
                    }
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);

                isTouchHandleChosen = false;
            }
        }
    }

    private void ThrowObjectIK()
    {        
        Throwable currentInteractable = (Throwable)interactableManager.CurrentInteractable;
        // pick up
        if (currentInteractable != null)
        {
            grabHandle = currentInteractable.GetGrabHandle();

            if (grabHandle.gameObject != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, grabHandle.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, grabHandle.rotation);

                currentInteractable.GetComponent<Rigidbody>().useGravity = false;
                currentInteractable.GetComponent<Rigidbody>().isKinematic = true;

                animator.SetLookAtPosition(grabHandle.position);
                animator.SetLookAtWeight(1);

                currentInteractable.transform.position = rightHandGrabHandle.position;
                currentInteractable.transform.SetParent(rightHandGrabHandle);


                animationManager.ExecuteThrowAnimation();
            }
        }
        
        // TODO Objekt anschauen nach aufnehmen --> Code refactoren? Interaktion aufteilen in Aufnehmen und Werfen?
        //else if (rightHand.GetComponentInChildren<Interactable>() != null)
        //{
        //    animator.SetIKPosition(AvatarIKGoal.RightHand, watchHandle.position);
        //    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
        //    animator.SetLookAtWeight(0.5f);

        //    animationManager.ExecuteThrowAnimation();
        //}
    }

    private void LeanIK()
    {
        Leanable currentInteractable = (Leanable)interactableManager.CurrentInteractable;

        // right hand
        closestPointRightHand = currentInteractable.SnapCollider.ClosestPoint(leanRightHand.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, closestPointRightHand);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, leanRightHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        // left hand
        closestPointLeftHand = currentInteractable.SnapCollider.ClosestPoint(leanLeftHand.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, closestPointLeftHand);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leanLeftHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        // viewing direction
        FaceWalkingDirection();
    }

    private void PassageLeanIK()
    {
        PassageLeanable currentInteractable = (PassageLeanable)interactableManager.CurrentInteractable;
        // right hand
        closestPointRightHand = currentInteractable.OppositeWall.ClosestPoint(passageLeanRightHand.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, closestPointRightHand);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, passageLeanRightHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        // left hand
        closestPointLeftHand = currentInteractable.OppositeWall.ClosestPoint(passageLeanLeftHand.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, closestPointLeftHand);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, passageLeanLeftHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        // viewing direction
        FaceWalkingDirection();
    }

    private void FaceWalkingDirection()
    {
        if (charController.XAxis * 2 >= 2)
        {
            animator.SetLookAtPosition(leftHandWatchHandle.position);
            animator.SetLookAtWeight(1);
        }
        else if (charController.XAxis * 2 <= -2)
        {
            animator.SetLookAtPosition(rightHandWatchHandle.position);
            animator.SetLookAtWeight(1);
        }
        else if (charController.XAxis == 0)
        {
            animator.SetLookAtPosition(watchHandle.position);
            animator.SetLookAtWeight(1);
        }
    }

    private void TouchIK()
    {
        Touchable currentInteractable = (Touchable)interactableManager.CurrentInteractable;

        List<Transform> touchHandles = currentInteractable.TouchHandles;


        if(isTouchHandleChosen == false)
        {
            closestTouchHandleLeft = FindClosestTouchHandleLeft(touchHandles);
            closestTouchHandleRight = FindClosestTouchHandleRight(touchHandles);
            isTouchHandleChosen = true;
            isLeftTouchHandleChosen = DecideTouchHandle(Vector3.Distance(leftHandGrabHandle.position, closestTouchHandleLeft.position), Vector3.Distance(rightHandGrabHandle.position, closestTouchHandleRight.position));
        }

        if(isLeftTouchHandleChosen == true)
        {
            // left
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, closestTouchHandleLeft.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, closestTouchHandleLeft.rotation);

            // right
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }
        else
        {
            // right
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, closestTouchHandleRight.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, closestTouchHandleRight.rotation);

            // left
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }

    private Transform FindClosestTouchHandleLeft(List<Transform> transforms)
    {
        float shortestDistanceLeft = 100f;
        Transform closestTransformLeft = null;
        foreach(Transform transform in transforms)
        {
            float distance = Vector3.Distance(leftHandGrabHandle.position, transform.position);
            if(distance < shortestDistanceLeft)
            {
                shortestDistanceLeft = distance;
                closestTransformLeft = transform;
            }
        }

        return closestTransformLeft;
    }

    private Transform FindClosestTouchHandleRight(List<Transform> transforms)
    {
        float shortestDistanceRight = 100f;
        Transform closestTransformRight = null;
        foreach (Transform transform in transforms)
        {
            float distance = Vector3.Distance(rightHandGrabHandle.position, transform.position);
            if (distance < shortestDistanceRight)
            {
                shortestDistanceRight = distance;
                closestTransformRight = transform;
            }
        }

        return closestTransformRight;
    }

    private bool DecideTouchHandle(float distanceLeft, float distanceRight)
    {
        if(distanceLeft < distanceRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
