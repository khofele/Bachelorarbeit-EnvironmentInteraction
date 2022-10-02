using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f * 2;

    private Vector3 velocity = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCrouching = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        moveDirection = transform.right * xAxis + transform.forward * zAxis;

        // Run
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Running");
            characterController.Move(moveDirection * (speed*2) * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

        // Crouch
        if(Input.GetKeyDown(KeyCode.C) && isCrouching == false)
        {
            isCrouching = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouching == true)
        {
            isCrouching = false;
        }


        if(isCrouching == true)
        {
            Debug.Log("Crouching");
            characterController.Move(moveDirection * (speed/2) * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        else if(isCrouching == false)
        {
            Debug.Log("Walking");
            characterController.Move(moveDirection * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}
