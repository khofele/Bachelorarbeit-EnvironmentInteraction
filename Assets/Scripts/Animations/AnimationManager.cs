using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ExecuteThrowAnimation()
    {
        animator.SetBool("isThrowing", true);
    }

    public void StopThrowAnimation()
    {
        animator.SetBool("isThrowing", false);
    }

    public void ExecuteRunAnimation(float zAxis, float xAxis)
    {
        animator.SetFloat("vertical", zAxis * 5.36f);
        animator.SetFloat("horizontal", xAxis * 0.81f);
    }

    public void ExecuteWalkAnimation(float zAxis, float xAxis)
    {
        animator.SetFloat("vertical", zAxis * 1.63f);
        animator.SetFloat("horizontal", xAxis * 0.5f);
    }

    public void ExecuteCrouchAnimation()
    {
        animator.SetBool("isCrouching", true);
    }

    public void StopCrouchAnimation()
    {
        animator.SetBool("isCrouching", false);
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    public void ExecuteCrouchLeanAnimation(float xAxis)
    {
        animator.SetBool("isCrouchingLean", true);
        animator.SetFloat("horizontal", xAxis * 2f);
    }
    
    public void StopCrouchLeanAnimation()
    {
        animator.SetBool("isCrouchingLean", false);
    }

    public void ExecuteStandLeanAnimation(float xAxis)
    {
        animator.SetBool("isCovering", true);
        animator.SetFloat("horizontal", xAxis * 2f);
    }

    public void StopStandLeanAnimation()
    {
        animator.SetBool("isCovering", false);
    }

    public void ExecuteJumpAnimation()
    {
        animator.SetTrigger("jump");
    }
}
