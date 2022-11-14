using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    // right hand
    [Header("Right Hand Transforms")]
    [SerializeField] private Transform rightHandGrabHandle = null;
    [SerializeField] private Transform rightHandLookTarget = null;
    [SerializeField] private Transform leanTargetRightHand = null;
    [SerializeField] private Transform passageLeanTargetRightHand = null;

    // left hand
    [Header("Left Hand Transforms")]
    [SerializeField] private Transform leftHandGrabHandle = null;
    [SerializeField] private Transform leftHandLookTarget = null;
    [SerializeField] private Transform leanTargetLeftHand = null;
    [SerializeField] private Transform passageLeanTargetLeftHand = null;

    // general
    private bool isIkActive = false;
    private Animator animator = null;
    private CharController charController = null;
    private InteractionManager interactionManager = null;
    private AnimationManager animationManager = null;
    private InteractableManager interactableManager = null;

    // throw
    private bool isThrowHandChosen = false;
    private Transform closestHand = null;
    private Transform grabHandleOfThrowable = null;

    // lean
    private Vector3 closestPointRightHand = Vector3.zero;
    private Vector3 closestPointLeftHand = Vector3.zero;

    // touch
    private bool isLeftTouchHandleChosen = false;
    private bool isTouchHandleChosen = false;
    private Transform closestTouchHandleRight = null;
    private Transform closestTouchHandleLeft = null;

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
                if (interactionManager.CurrentInteraction != null)
                {
                    GameObject currentInteractionGameObject = interactionManager.CurrentInteraction.gameObject;

                    // Throw
                    if (currentInteractionGameObject.GetComponent<ThrowObjectInteraction>() != null)
                    {
                        ThrowObjectIK();
                    }
                    // Lean: Passage
                    else if (currentInteractionGameObject.GetComponent<PassageLeanInteraction>() != null)
                    {
                        PassageLeanIK();
                    }
                    // Lean: Crouch, on Edge and Stand
                    else if (currentInteractionGameObject.GetComponent<LeanInteraction>() != null)
                    {
                        LeanIK();
                    }
                    // Touch door
                    else if (currentInteractionGameObject.GetComponent<TouchObjectInteraction>() != null)
                    {
                        TouchIK();
                    }
                    // Jump over
                    else if (currentInteractionGameObject.GetComponent<JumpOverObstacleInteraction>() != null)
                    {
                        JumpIK();
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
                if(isThrowHandChosen == false)
                {
                    isThrowHandChosen = true;
                    closestHand = GetClosestHand();
                }

                if (closestHand == rightHandGrabHandle)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, grabHandleOfThrowable.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, grabHandleOfThrowable.rotation);
                }
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, grabHandleOfThrowable.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, grabHandleOfThrowable.rotation);
                }

                currentInteractable.GetComponent<Rigidbody>().useGravity = false;
                currentInteractable.GetComponent<Rigidbody>().isKinematic = true;

                animator.SetLookAtPosition(grabHandleOfThrowable.position);
                animator.SetLookAtWeight(1);


                currentInteractable.transform.position = closestHand.position;
                currentInteractable.transform.SetParent(closestHand);

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

    private void LeanIK()
    {
        Leanable currentInteractable = (Leanable)interactableManager.CurrentInteractable;

        // right hand
        closestPointRightHand = currentInteractable.SnapCollider.ClosestPoint(leanTargetRightHand.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, closestPointRightHand);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, leanTargetRightHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        // left hand
        closestPointLeftHand = currentInteractable.SnapCollider.ClosestPoint(leanTargetLeftHand.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, closestPointLeftHand);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leanTargetLeftHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        // viewing direction
        FaceDirection();
    }

    private void PassageLeanIK()
    {
        PassageLeanable currentInteractable = (PassageLeanable)interactableManager.CurrentInteractable;
        // right hand
        closestPointRightHand = currentInteractable.OppositeWall.ClosestPoint(passageLeanTargetRightHand.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, closestPointRightHand);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, passageLeanTargetRightHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        // left hand
        closestPointLeftHand = currentInteractable.OppositeWall.ClosestPoint(passageLeanTargetLeftHand.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, closestPointLeftHand);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, passageLeanTargetLeftHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        // viewing direction
        ReverseFaceDirection();
    }

    private void FaceDirection()
    {
        if (charController.XAxis == 1)
        {
            animator.SetLookAtPosition(leftHandLookTarget.position);
        }
        else if (charController.XAxis == -1)
        {
            animator.SetLookAtPosition(rightHandLookTarget.position);
        }

        animator.SetLookAtWeight(1f);
    }    
    
    private void ReverseFaceDirection()
    {
        if (charController.XAxis == -1)
        {
            animator.SetLookAtPosition(leftHandLookTarget.position);
        }
        else if (charController.XAxis == 1)
        {
            animator.SetLookAtPosition(rightHandLookTarget.position);
        }

        animator.SetLookAtWeight(1f);
    }

    private void TouchIK()
    {
        Touchable currentInteractable = (Touchable)interactableManager.CurrentInteractable;

        List<Transform> touchHandles = currentInteractable.TouchHandles;


        if (isTouchHandleChosen == false)
        {
            closestTouchHandleLeft = FindClosestTouchHandleLeft(touchHandles);
            closestTouchHandleRight = FindClosestTouchHandleRight(touchHandles);
            isTouchHandleChosen = true;
            isLeftTouchHandleChosen = DecideTouchHandle(Vector3.Distance(leftHandGrabHandle.position, closestTouchHandleLeft.position), Vector3.Distance(rightHandGrabHandle.position, closestTouchHandleRight.position));
        }

        if (isLeftTouchHandleChosen == true)
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
            if (distance < shortestDistanceLeft)
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
        if (distanceLeft < distanceRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void JumpIK()
    {
        Jumpable currentInteractable = (Jumpable)interactableManager.CurrentInteractable;

        Collider jumpCollider = currentInteractable.JumpCollider;

        Vector3 closestPointRightHand = jumpCollider.ClosestPoint(rightHandGrabHandle.position);
        Vector3 closestPointLeftHand = jumpCollider.ClosestPoint(leftHandGrabHandle.position);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, closestPointRightHand);
        animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(closestPointRightHand));

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, closestPointLeftHand);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.LookRotation(closestPointLeftHand));
    }

    public void SetLookAtWeight(float weight)
    {
        animator.SetLookAtWeight(weight);
    }
}
