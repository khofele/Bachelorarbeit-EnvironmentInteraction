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

    public void ExecuteThrowAnimation(string animationBool)
    {
        animator.SetBool(animationBool, true);
    }

    public void StopThrowAnimation()
    {
        animator.SetBool("isThrowingLeft", false);
        animator.SetBool("isThrowingRight", false);
    }

    public void ExecuteRunAnimation(float zAxis, float xAxis)
    {
        animator.SetFloat("vertical", zAxis);
        animator.SetFloat("horizontal", xAxis * 0.5f);
    }

    public void ExecuteWalkAnimation(float zAxis, float xAxis)
    {
        animator.SetFloat("vertical", zAxis);
        animator.SetFloat("horizontal", xAxis * 0.5f);
    }

    public void ExecuteCrouchAnimation(float zAxis, float xAxis)
    {
        animator.SetBool("isCrouching", true);
        animator.SetFloat("vertical", zAxis);
        animator.SetFloat("horizontal", xAxis*0.5f);
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
        animator.SetFloat("horizontal", xAxis*0.5f);
    }

    public void StopStandLeanAnimation()
    {
        animator.SetBool("isCovering", false);
    }

    public void ExecuteJumpAnimation()
    {
        animator.SetTrigger("jump");
        animator.SetBool("isJumping", true);
    }

    public void StopJumpAnimation()
    {
        animator.SetBool("isJumping", false);
    }

    public void ExecuteDuckingAnimation()
    {
        animator.SetBool("isDucking", true);
    }

    public void StopDuckingAnimation()
    {
        animator.SetBool("isDucking", false);
    }

    public void EnableUpperBodyLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), 1f);
    }

    public void DisableUpperBodyLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), 0f);
    }

    public void EnableArmsLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Arms"), 1f);
    }

    public void DisableArmsLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Arms"), 0f);
    }

    public void ExecuteCrossPunchLeft()
    {
        animator.SetTrigger("crossPunchLeft");
    }

    public void ExecuteCrossPunchRight()
    {
        animator.SetTrigger("crossPunchRight");
    }

    public void ExecuteBasicHipPunchLeft()
    {
        animator.SetTrigger("basicHipPunchLeft");
    }

    public void ExecuteBasicHipPunchRight()
    {
        animator.SetTrigger("basicHipPunchRight");
    }

    public void ExecuteBasicPunchLeft()
    {
        animator.SetTrigger("basicPunchLeft");
    }

    public void ExecuteBasicPunchRight()
    {
        animator.SetTrigger("basicPunchRight");
    }

    public void EnableHeadLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Head"), 1f);
    }

    public void ExecuteStomp()
    {
        animator.SetTrigger("stomp");
    }

    public void ExecutePush()
    {
        animator.SetTrigger("push");
    }

    public void ExecuteIdle()
    {
        animator.SetTrigger("idle");
    }
}
