using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private Transform rightHand = null;
    [SerializeField] private GameObject interactables = null;
    [SerializeField] private Transform watchHandle = null;

    private bool isIkActive = false;
    private Animator animator = null;
    private Transform grabHandle = null;
    private InteractionManager interactionManager = null;
    private AnimationManager animationManager = null;
    private InteractableManager interactableManager = null;

    public Transform RightHand { get => rightHand; }
    public GameObject Interactables { get => interactables; }
    public Transform WatchHandle { get => watchHandle; }
    public bool IsIkActive { get => isIkActive; set => isIkActive = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        animationManager = GetComponent<AnimationManager>();
        interactableManager = FindObjectOfType<InteractableManager>();
    }

    // TODO nur IK-relevante Methoden aufrufen 
    // TODO aufräumen --> je nach bool anderes IK-Event, ggf. mit States arbeiten
    private void OnAnimatorIK()
    {
        if (animator == true)
        {
            if (isIkActive == true)
            {
                if(interactionManager.CurrentInteraction != null)
                {
                    if (interactionManager.CurrentInteraction.gameObject.GetComponent<ThrowObjectInteraction>() != null)
                    {
                        ThrowObjectIK();
                    }
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }

    private void ThrowObjectIK()
    {
        Throwable currentInteractable = (Throwable) interactableManager.CurrentInteractable;
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

                currentInteractable.transform.position = rightHand.position;
                currentInteractable.transform.SetParent(rightHand);


                animationManager.ExecuteThrowAnimation();
            }
        }

        // TODO Objekt anschauen nach aufnehmen --> Code refactoren
        //else if (rightHand.GetComponentInChildren<Interactable>() != null)
        //{
        //    animator.SetIKPosition(AvatarIKGoal.RightHand, watchHandle.position);
        //    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
        //    animator.SetLookAtWeight(0.5f);

        //    animationManager.ExecuteThrowAnimation();
        //}
    }
}
