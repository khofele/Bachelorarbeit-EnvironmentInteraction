using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private Transform grabHandle = null;
    [SerializeField] private Transform rightHand = null;
    [SerializeField] private GameObject environment = null;
    [SerializeField] private Transform watchHandle = null;

    private bool isIkActive = false;
    private CharController charController = null;
    private Animator animator = null;

    public Transform GrabHandle { get => grabHandle; }
    public Transform RightHand { get => rightHand; }
    public GameObject Environment { get => environment; }
    public Transform WatchHandle { get => watchHandle; }
    public bool IsIkActive { get => isIkActive; set => isIkActive = value; }

    private void Start()
    {
        charController = GetComponent<CharController>();
        animator = GetComponent<Animator>();
    }

    // TODO nur IK-relevante Methoden aufrufen 
    // TODO aufräumen --> je nach bool anderes IK-Event, ggf. mit States arbeiten
    private void OnAnimatorIK()
    {
        if (animator == true)
        {
            if (isIkActive == true)
            {
                
                if (charController.CurrentInteractable != null)
                {
                    animator.SetLookAtWeight(1);
                    // TODO GrabHandle von ThrowableObjects bekommen
                    if (grabHandle.gameObject != null)
                    {
                        charController.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        charController.Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
                        charController.Animator.SetIKPosition(AvatarIKGoal.RightHand, grabHandle.position);
                        charController.Animator.SetIKRotation(AvatarIKGoal.RightHand, grabHandle.rotation);

                        charController.CurrentInteractable.GetComponent<Rigidbody>().useGravity = false;
                        charController.CurrentInteractable.GetComponent<Rigidbody>().isKinematic = true;

                        charController.CurrentInteractable.transform.position = rightHand.position;
                        charController.CurrentInteractable.transform.SetParent(rightHand);

                        isIkActive = false;
                        animator.SetBool("isInteracting", false);

                    }
                }
            }
            else if (rightHand.GetComponentInChildren<Interactable>() != null)
            {
                animator.SetIKPosition(AvatarIKGoal.RightHand, watchHandle.position);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
                animator.SetLookAtWeight(0);

                animator.SetBool("isInteracting", true);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
