using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // General fields
    [SerializeField] private Transform groundCheck = null;
    private float groundDistance = 0.4f;
    private LayerMask groundMask;
    private bool isGrounded = false;
    private CharacterController characterController = null;
    private AnimationManager animationManager = null;
    private InteractionManager interactionManager = null;
    private TriggerCheckManager triggerCheckManager = null;

    // Movement fields
    private float speed = 0f;
    private float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCrouching = false;
    private bool isWalking = false;
    private float xAxis = 0f;
    private float zAxis = 0f;

    public bool IsCrouching { get => isCrouching; }
    public bool IsWalking { get => isWalking; }
    public float XAxis { get => xAxis; set => xAxis = value; }
    public float ZAxis { get => zAxis; }
    public Vector3 MoveDirection { get => moveDirection; }
    public TriggerCheckManager TriggerCheckManager { get => triggerCheckManager; }

    private void Awake()
    {
        triggerCheckManager = GetComponentInChildren<TriggerCheckManager>();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animationManager = GetComponent<AnimationManager>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        groundMask = LayerMask.NameToLayer("Environment");

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Limbs"), LayerMask.NameToLayer("Default"));

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, -groundMask);

        if (isGrounded && velocity.y < 0 && interactionManager.IsJumping == false && interactionManager.IsClimbing == false)
        {
            velocity.y = -2f;
        }

        if(interactionManager.IsClimbing == true)
        {
            moveDirection = new Vector3(-xAxis, zAxis, 0f).normalized;

            if (zAxis > 0f)
            {
                velocity.y = 0.025f;
            }
            else if (zAxis < 0f)
            {
                velocity.y = -0.025f;
            }
            else
            {
                velocity.y = 0f;
            }
        }
        else
        {        
            moveDirection = new Vector3(xAxis, 0f, zAxis).normalized;
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        }

        if (interactionManager.IsFixedLeaning == true)
        {
            zAxis = 0;
        }
        else
        {
            zAxis = Input.GetAxis("Vertical");
        }

        if (interactionManager.IsJumping == true || interactionManager.IsFighting == true)
        {
            xAxis = 0;
            zAxis = 0;
        }
        else
        {
            xAxis = Input.GetAxis("Horizontal");
        }

        if (interactionManager.IsLeaning == false && interactionManager.IsJumping == false && interactionManager.IsFighting == false && interactionManager.IsClimbing == false)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized, Camera.main.transform.up);
        }

        // check if button is pressed
        if (moveDirection.magnitude >= 0.01f)
        {
            if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false && interactionManager.IsClimbing == false)
            {
                // Running
                speed = 10f;
                animationManager.ExecuteRunAnimation(zAxis, xAxis);
            } 
            else if (isCrouching == true)
            {
                // Crouching
                speed = 2.5f;
                animationManager.ExecuteCrouchAnimation(zAxis, xAxis);
            }
            else
            {
                // Walking
                speed = 3f;
                animationManager.ExecuteWalkAnimation(zAxis, xAxis);
            }
        }
        else
        {
            // Idle
            speed = 0;
        }

        // move 
        if (interactionManager.IsFighting == false && interactionManager.IsJumping == false && interactionManager.IsSnapping == false)
        {
            characterController.Move(speed * Time.deltaTime * moveDirection);

            if (interactionManager.IsClimbing == false)
            {
                velocity.y += gravity * Time.deltaTime;
            }

            characterController.Move(velocity * Time.deltaTime);
        }

        animationManager.SetSpeed(speed);

        // Crouch Toggle
        if (Input.GetKeyDown(KeyCode.C) && isCrouching == false)
        {
            isCrouching = true;
            animationManager.ExecuteCrouchAnimation(zAxis, xAxis);
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouching == true)
        {
            isCrouching = false;
            animationManager.StopCrouchAnimation();
        }

        // Walking bool Toggle
        if (speed == 3f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
