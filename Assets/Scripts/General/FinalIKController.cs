using RootMotion.FinalIK;
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

    // DEBUG
    [SerializeField] private Transform hitSpot = null;
    // DEBUG

    // general
    private FullBodyBipedIK fullBodyIK = null;
    private LookAtIK lookAtIK = null;
    private bool isIkActive = false;
    private CharController charController = null;
    private InteractionManager interactionManager = null;
    private AnimationManager animationManager = null;
    private InteractableManager interactableManager = null;
    private MultipleOutcomesInteraction multipleOutcomesInteraction = null;
    private Interaction currentInteraction = null;

    // throw
    private bool isThrowHandChosen = false;
    private Transform closestHand = null;
    private Transform grabHandleOfThrowable = null;

    // touch
    private bool isLeftTouchHandleChosen = false;
    private bool isTouchHandleChosen = false;
    private Transform closestTouchHandleRight = null;
    private Transform closestTouchHandleLeft = null;

    // fight
    private bool isClosestPointFound = false;
    private Vector3 closestPointRight = Vector3.zero;
    private Vector3 closestPointLeft = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

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
                    if (interactionManager.CurrentInteraction.GetComponent<MultipleOutcomesInteraction>() != null)
                    {
                        multipleOutcomesInteraction = (MultipleOutcomesInteraction)interactionManager.CurrentInteraction;
                    }
                    else
                    {
                        multipleOutcomesInteraction = null;
                    }

                    currentInteraction = interactionManager.CurrentInteraction;

                    if (multipleOutcomesInteraction == null)
                    {
                        // Throw
                        if (currentInteraction.GetType() == typeof(ThrowObjectInteraction))
                        {
                            ThrowObjectIK();
                        }
                        // Lean: Passage
                        else if (currentInteraction.GetType() == typeof(PassageLeanInteraction))
                        {
                            PassageLeanIK();
                        }
                        // Lean: Crouch, on Edge and Stand
                        else if (currentInteraction.GetType() == typeof(LeanInteraction))
                        {
                            LeanIK();
                        }
                        // Touch object
                        else if (currentInteraction.GetType() == typeof(TouchObjectInteraction))
                        {
                            TouchIK();
                        }
                        // Jump over
                        else if (currentInteraction.GetType() == typeof(JumpOverObstacleInteraction))
                        {
                            JumpIK();
                        }
                    }
                    // Multiple Outcomes Interactions
                    // Fistfight + Outcomes
                    else
                    {
                        if(multipleOutcomesInteraction.OutcomeManager.CurrentOutcome != null)
                        {
                            // Strike head
                            if (multipleOutcomesInteraction.OutcomeManager.CurrentOutcome.GetType() == typeof(StrikeEnemyOnObjectOutcome))
                            {
                                StrikeBodyIK();
                            }
                            else if (multipleOutcomesInteraction.OutcomeManager.CurrentOutcome.GetType() == typeof(StompOnEnemyOutcome))
                            {
                                StompOnEnemyIK();
                            }
                        }
                        // Fistfight
                        else if (currentInteraction.GetType() == typeof(FistFightInteraction))
                        {
                            FistFightIK();
                        }
                    }


                }
            }
            else
            {
                Reset();
                isThrowHandChosen = false;
                isTouchHandleChosen = false;
                isClosestPointFound = false;        
            }
        }
    }

    // ---------- THROW -------------------------------------------------------------------------------------

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
                    fullBodyIK.solver.rightHandEffector.position = grabHandleOfThrowable.position;
                    fullBodyIK.solver.rightHandEffector.rotation = grabHandleOfThrowable.rotation;
                    fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
                    fullBodyIK.solver.rightHandEffector.rotationWeight = 1f;
                }
                else
                {
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

    // ---------- PASSAGE LEAN -----------------------------------------------------------------------------
    private void PassageLeanIK()
    {
        PassageLeanable currentInteractable = (PassageLeanable)interactableManager.CurrentInteractable;

        // right hand
        Vector3 closestPointRightHand = currentInteractable.OppositeWall.ClosestPoint(passageLeanTargetRightHand.position);
        fullBodyIK.solver.rightHandEffector.position = closestPointRightHand;
        fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
        fullBodyIK.solver.rightHandEffector.rotation = passageLeanTargetRightHand.rotation;
        fullBodyIK.solver.rightHandEffector.rotationWeight = 1f;

        // left hand
        Vector3 closestPointLeftHand = currentInteractable.OppositeWall.ClosestPoint(passageLeanTargetLeftHand.position);
        fullBodyIK.solver.leftHandEffector.position = closestPointLeftHand;
        fullBodyIK.solver.leftHandEffector.positionWeight = 1f;
        fullBodyIK.solver.leftHandEffector.rotation = passageLeanTargetLeftHand.rotation;
        fullBodyIK.solver.leftHandEffector.rotationWeight = 1f;

        // viewing direction
        FaceDirection();
    }

    // ---------- LEAN -------------------------------------------------------------------------------------

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

    // ---------- JUMP -------------------------------------------------------------------------------------

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
            fullBodyIK.solver.leftHandEffector.position = closestTouchHandleLeft.position;
            fullBodyIK.solver.leftHandEffector.rotation = closestTouchHandleLeft.rotation;
            fullBodyIK.solver.leftHandEffector.positionWeight = 1f;
            fullBodyIK.solver.leftHandEffector.rotationWeight = 0.5f;

            // right
            fullBodyIK.solver.rightHandEffector.positionWeight = 0;
            fullBodyIK.solver.rightHandEffector.rotationWeight = 0;
        }
        else
        {
            // right
            fullBodyIK.solver.rightHandEffector.position = closestTouchHandleRight.position;
            fullBodyIK.solver.rightHandEffector.rotation = closestTouchHandleRight.rotation;
            fullBodyIK.solver.rightHandEffector.positionWeight = 1f;
            fullBodyIK.solver.rightHandEffector.rotationWeight = 0.5f;

            // left
            fullBodyIK.solver.leftHandEffector.positionWeight = 0;
            fullBodyIK.solver.leftHandEffector.rotationWeight = 0;
        }
    }

    private Transform FindClosestTouchHandleLeft(List<Transform> transforms)
    {
        float shortestDistanceLeft = 100f;
        Transform closestTransformLeft = null;
        foreach (Transform transform in transforms)
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

    // ---------- JUMP -------------------------------------------------------------------------------------

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
        fullBodyIK.solver.leftHandEffector.rotation = Quaternion.Euler(60f, 230f, 104f);  // linke Hand dreht sich sonst seltsam
        fullBodyIK.solver.leftHandEffector.rotationWeight = 1f;
    }

    // ---------- FIST FIGHT --------------------------------------------------------------------------------

    private void FistFightIK()
    {
        Enemy currentInteractable = (Enemy)interactableManager.CurrentInteractable;
        CapsuleCollider bodyCollider = currentInteractable.BodyCollider;

        FistFightInteraction currentInteraction = (FistFightInteraction)interactionManager.CurrentInteraction;

        if(isClosestPointFound == false)
        {
            isClosestPointFound = true;
            closestPointRight = bodyCollider.ClosestPoint(fullBodyIK.solver.rightHandEffector.position);
            closestPointLeft = bodyCollider.ClosestPoint(fullBodyIK.solver.leftHandEffector.position);
        }

        if (currentInteraction.CurrentHand == Hands.RIGHT && isClosestPointFound == true)
        {
            fullBodyIK.solver.rightHandEffector.position = closestPointRight;
        }
        else if (currentInteraction.CurrentHand == Hands.LEFT)
        {
            fullBodyIK.solver.leftHandEffector.position = closestPointLeft;
        }

        LookAtCurrentInteractable(currentInteractable);
    }

    public void SetRightHandPositionWeightToMin()
    {
        fullBodyIK.solver.rightHandEffector.positionWeight = 0f;
    }

    // Strike Head Outcome
    private void StrikeBodyIK()
    {
        FistFightInteraction currentInteraction = (FistFightInteraction)interactionManager.CurrentInteraction;
        StrikeEnemyOnObjectOutcome currentOutcome = (StrikeEnemyOnObjectOutcome)currentInteraction.OutcomeManager.CurrentOutcome;

        Collider targetCollider = currentOutcome.Target.GetComponent<Collider>();
        
        targetPosition = targetCollider.ClosestPoint(fullBodyIK.solver.rightHandEffector.position);
        fullBodyIK.solver.rightHandEffector.position = targetPosition;
    }


    // Stomp on Enemy Outcome
    private void StompOnEnemyIK()
    {
        Enemy currentInteractable = (Enemy)interactableManager.CurrentInteractable;

        fullBodyIK.solver.rightFootEffector.position = hitSpot.transform.position;

        LookAtCurrentInteractable(currentInteractable);
    }

    // ---------- ADDITIONAL METHODS ------------------------------------------------------------------------

    private void FaceDirection()
    {
        if (charController.XAxis == 1)
        {
            lookAtIK.solver.target = leftHandLookTarget;
            lookAtIK.solver.IKPositionWeight = 1f;
        }
        else if (charController.XAxis == -1)
        {
            lookAtIK.solver.target = rightHandLookTarget;
            lookAtIK.solver.IKPositionWeight = 1f;
        }
        else
        {
            lookAtIK.solver.target = null;
            lookAtIK.solver.IKPositionWeight = 0f;
        }
    }

    private void LookAtCurrentInteractable(Interactable currentInteractable)
    {
        lookAtIK.solver.target = currentInteractable.transform;
        lookAtIK.solver.IKPositionWeight = 1f;
        lookAtIK.solver.headWeight = 0.4f;
    }

    private void Reset()
    {
        fullBodyIK.solver.leftHandEffector.positionWeight = 0f;
        fullBodyIK.solver.leftHandEffector.rotationWeight = 0f;
        fullBodyIK.solver.rightHandEffector.positionWeight = 0f;
        fullBodyIK.solver.rightHandEffector.rotationWeight = 0f;
        lookAtIK.solver.target = null;
        lookAtIK.solver.IKPositionWeight = 0f;
        lookAtIK.solver.headWeight = 0f;
        isIkActive = false;
    }
}
