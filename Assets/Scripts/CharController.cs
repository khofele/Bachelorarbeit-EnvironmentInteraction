using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float gravity = -9.81f * 2;

    private Vector3 velocity = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCrouching = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        moveDirection = new Vector3(xAxis, 0f, zAxis).normalized;

        // check if button is pressed
        if(moveDirection.magnitude >= 0.01f)
        {

            if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
            {
                // Running
                speed = 10f;
            }
            else if (isCrouching == true)
            {
                // Crouching
                speed = 2.5f;
            }
            else
            {
                // Walking
                speed = 5f;
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(moveDirection * speed * Time.deltaTime);
            characterController.Move(velocity * Time.deltaTime);

        }
        else
        {
            speed = 0;
        }


        // Crouch Toggle
        if (Input.GetKeyDown(KeyCode.C) && isCrouching == false)
        {
            isCrouching = true;
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouching == true)
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);
        }

        animator.SetFloat("speed", speed);
        Debug.Log(speed);
    }
}
