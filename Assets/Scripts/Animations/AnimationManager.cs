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

    public void ExecuteCrouchAndLeanAnimation(float zAxis, float xAxis)
    {
        animator.SetFloat("vertical", zAxis * 0.27f);
        animator.SetFloat("horizontal", xAxis * 1.026f);
    }

    public void ExecuteCrouchAndLeanAnimation()
    {
        animator.SetBool("isCrouchingLean", true);
    }    
    
    public void StopCrouchAndLeanAnimation()
    {
        animator.SetBool("isCrouchingLean", false);
    }
}
