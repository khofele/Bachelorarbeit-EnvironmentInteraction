using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // General fields
    private CharacterController characterController = null;
    private Animator animator = null;
    private IKController iKController = null;
    private AnimationManager animationManager = null;

    // Movement fields
    private float speed = 0f;
    private float gravity = -9.81f * 2;
    private Vector3 velocity = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCrouching = false;

    // Interaction fields
    private InteractionManager interactionManager = null;
    private Interactable currentInteractable = null;

    public Animator Animator { get => animator; }
    public Interactable CurrentInteractable { get => currentInteractable; }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        iKController = GetComponent<IKController>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        animationManager = GetComponent<AnimationManager>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        // Movedirection + rotation --> follow camera rotation
        moveDirection = new Vector3(xAxis, 0f, zAxis).normalized;
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized, Camera.main.transform.up);

        // check if button is pressed
        if (moveDirection.magnitude >= 0.01f)
        {
            if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
            {
                // Running
                speed = 10f;
                animationManager.ExecuteRunAnimation(zAxis, xAxis);
            }
            else if (isCrouching == true)
            {
                // Crouching
                speed = 2.5f;
            }
            else
            {
                // Walking
                speed = 3f;
                animationManager.ExecuteWalkAnimation(zAxis, xAxis);
            }

            // move character
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(moveDirection * speed * Time.deltaTime);
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            // Idle
            speed = 0;
        }

        // Crouch Toggle
        if (Input.GetKeyDown(KeyCode.C) && isCrouching == false)
        {
            isCrouching = true;
            animationManager.ExecuteCrouchAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouching == true)
        {
            isCrouching = false;
            animationManager.StopCrouchAnimation();
        }

        animationManager.SetSpeed(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Interactable>() != null && other.gameObject.GetComponent<Interactable>() != currentInteractable && interactionManager.IsTriggered == false)
        {
            // TODO: verallgemeinern? hat jede Interaktion ein Interactable?
            currentInteractable = other.gameObject.GetComponent<Interactable>();
            interactionManager.IsTriggered = true;
        }
    }
}
