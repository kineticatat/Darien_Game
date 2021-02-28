using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    #region Singleton

    public static FirstPersonController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Camera mainCam;
    public Vector2 xyRotationSpeed;
    public float speed;

    public Interactable currentInteractable;

    public bool canMove;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        TestInteract();
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                MoveCharacter();
            }

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                RotateCharacter();
            }

            if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
            {
                Interact();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                MapUI.instance.ToggleMap(true);
            }
        }
    }

    public void ToggleMovement(bool toggle)
    {
        canMove = toggle;
    }

    private void TestInteract()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactableItem = hit.transform.GetComponent<Interactable>();

            if (interactableItem != null)
            {
                
                currentInteractable = interactableItem;
                currentInteractable.DisplayInteractionText(true);
            }
            else
            {
                if (currentInteractable != null) currentInteractable.DisplayInteractionText(false);

                currentInteractable = null;
            }
        }

    }

    public void Interact()
    {
        currentInteractable.Interact();
    }

    public void RotateCharacter()
    {
        float yRotation = Input.GetAxis("Mouse X") * xyRotationSpeed.y * Time.deltaTime;
        float xRotation = Input.GetAxis("Mouse Y") * xyRotationSpeed.x * Time.deltaTime;

        transform.Rotate(new Vector3(0, yRotation, 0));

        if (true)//test to make sure rotation is properly clamped
        {
            mainCam.transform.Rotate(xRotation, 0, 0);
        }
    }

    public void MoveCharacter()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movementDirection = new Vector3(xMovement, 0, zMovement);

        transform.Translate(movementDirection);
    }

}
