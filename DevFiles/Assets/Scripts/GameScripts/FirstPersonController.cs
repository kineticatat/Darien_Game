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

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            MoveCharacter();
        }

        if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
        {
            RotateCharacter();
        }
    }

    public void RotateCharacter()
    {
        
    }

    public void MoveCharacter()
    {
        float XMovement = Input.GetAxis("Horizontal");
        float ZMovement = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(Input.GetAxis())
    }

}
