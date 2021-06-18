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

   [SerializeField] private GameObject mainCam;
    public Vector2 xyRotationSpeed;
    public float speed;

    public float interactionDistance = 5f;
    public Interactable currentInteractable;

    public bool canMove;
    public bool isMoving;
    public string walkingSFX;

    private float yRotation;
    private float xRotation;


    private void Start()
    {
        //mainCam = Camera.main;
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
                if (walkingSFX != "" && !isMoving)
                {
                    AudioManager.instance.Play(walkingSFX);
                    isMoving = true;
                }
            }

            if (isMoving && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                isMoving = false;
                AudioManager.instance.Stop(walkingSFX);
            }

            //if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
           // {
                RotateCharacter();
           // }

            if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
            {
                Interact();
            }

            

        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            if (MapUI.instance.mapObject.activeSelf)
            {
                AudioManager.instance.Play("map_close");
                MapUI.instance.ToggleMap(false);
            }
            else
            {
                AudioManager.instance.Play("map_open");
                MapUI.instance.ToggleMap(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            if (Notebook.instance.notebookUI.activeSelf)
            {
                AudioManager.instance.Play("map_close");
                Notebook.instance.notebookUI.SetActive(false);
                ToggleMovement(true);

            }
            else
            {
                AudioManager.instance.Play("map_open");
                Notebook.instance.notebookUI.SetActive(true);
                Notebook.instance.DisplayEntries(Notebook.instance.currentEntry);
                ToggleMovement(false);
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
        if (Physics.Raycast(ray, out hit, interactionDistance))
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
         yRotation += Input.GetAxis("Mouse X") * xyRotationSpeed.y * Time.deltaTime;
         xRotation += Input.GetAxis("Mouse Y") * xyRotationSpeed.x * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        mainCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }

    public void MoveCharacter()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movementDirection = new Vector3(xMovement, 0, zMovement);

        transform.Translate(movementDirection);
    }

}
