using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Controller : MonoBehaviour
{
    #region Singleton

    public static Movement_Controller instance;

    private void Awake()
    {
        controls = new InputMaster();
        instance = this;
    }

    #endregion

    public bool isClimbing;

    public float speed;
    public float sprintModifier;
    private float maximumMomevemt;
    private float currentSpeed;

    private Animator animator;
    private new Rigidbody rigidbody;

    public Transform playerModelTransform;
    public Camera playerCamera;
    public Transform cameraRootTransform;
    public Transform cameraFocusPoint;

    public Vector2 minMaxCameraZoom;
    public float zoomSpeed = 3;
    public float cameraRotateSpeed = 3;

    public bool isRolling;
    public InputMaster controls;
    private Quaternion preRollRotation;
    private Vector3 rollDirectionPosition;
    public float rollSpeed;

    public bool isGrounded;
    private bool hasJumped;
    private float previousYposition;
    public float jumpForce;
    [Range(0, 1)]
    public float forwardJumpMomentum;

    public bool canMove = true;

    private void Start()
    {
        currentSpeed = speed;
        maximumMomevemt = speed * sprintModifier;

        animator = GetComponentInChildren<Animator>();

        controls.Enable();
        controls.HackSlash.Roll.performed += context => Roll(context.ReadValue<Vector2>());

        previousYposition = transform.position.y;
        isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();

        canMove = true;
        isClimbing = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            CheckSprint();
            CheckMovement();
            CheckJump();
        }

        CheckCameraRotation();
    }

    private void CheckMovement()
    {
        if (isGrounded && !isRolling && Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            transform.Translate(movement * Time.deltaTime * currentSpeed);

            float angle = Mathf.Acos(Vector3.Dot(movement, Vector3.forward)) * 180 * Mathf.Sign(movement.x) / 3.1415926f;

            Quaternion eulerRotation = Quaternion.Euler(0, angle, 0);
            playerModelTransform.rotation = eulerRotation;

            float percentMaximumMovement = (movement * currentSpeed).sqrMagnitude / (maximumMomevemt * maximumMomevemt);
            animator.SetFloat("Speed", percentMaximumMovement, .03f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0, .1f, Time.deltaTime);

            if (isRolling)
            {
                transform.Translate(rollDirectionPosition * Time.deltaTime * maximumMomevemt);
            }
        }
    }

    private void CheckSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = sprintModifier * speed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = speed;
        }
    }

    private void CheckCameraRotation()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            playerCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, minMaxCameraZoom.x, minMaxCameraZoom.y);
        }


        if (Input.GetKey(KeyCode.Mouse2))
        {
            cameraRootTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")) * cameraRotateSpeed);

            playerCamera.transform.Translate(new Vector3(0, -Input.GetAxis("Mouse Y")) * cameraRotateSpeed / 3, Space.World);
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, Mathf.Clamp(playerCamera.transform.position.y, .5f, 20), playerCamera.transform.position.z);

            playerCamera.transform.LookAt(cameraFocusPoint);
        }
        else
        {
            cameraRootTransform.rotation = Quaternion.identity;
        }
    }

    private void Roll(Vector2 vector2)
    {
        if (isGrounded)
        {
            rollDirectionPosition = playerModelTransform.forward;
            animator.applyRootMotion = true;
            animator.SetTrigger("Rolling");
            isRolling = true;
            preRollRotation = playerModelTransform.rotation;
        }
    }

    public void EndRoll()
    {
        animator.applyRootMotion = false;
        isRolling = false;
        playerModelTransform.localPosition = Vector3.zero;
        playerModelTransform.rotation = preRollRotation;
    }

    public bool GroundedCheck()
    {

        if (Physics.Raycast(transform.position + new Vector3(0,.1f,0) , -transform.up,2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void CheckJump()
    {
        if (!hasJumped)
        {
            isGrounded = GroundedCheck();
            animator.SetBool("IsGrounded", isGrounded);
        }

        if (!isRolling)
        {
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                hasJumped = true;
                animator.SetTrigger("Jump");
                animator.SetBool("IsGrounded", false);
            }

            previousYposition = transform.position.y;
        }
    }

    public void Jump()
    {
        if (GroundedCheck())
        {
            //rigidbody.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
            if ((playerModelTransform.localEulerAngles.y > 265 && playerModelTransform.localEulerAngles.y < 275)||
                (playerModelTransform.localEulerAngles.y < 95 && playerModelTransform.localEulerAngles.y > 85))
            {
                rigidbody.AddForce(new Vector3(0, jumpForce) + playerModelTransform.forward * currentSpeed * 1.16f, ForceMode.Impulse);
            }
            else
            {
                rigidbody.AddForce(new Vector3(0, jumpForce) + playerModelTransform.forward * jumpForce * forwardJumpMomentum, ForceMode.Impulse);
            }
            isGrounded = false;
            StartCoroutine(JumpDelay());
        }
        else
        {
            hasJumped = false;
        }
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(.3f);
        hasJumped = false;
        CheckSprint();
    }

    public void SetMeshHeight(float height)
    {
        playerModelTransform.localPosition = new Vector3(playerModelTransform.localPosition.x, height, playerModelTransform.localPosition.y);
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}
