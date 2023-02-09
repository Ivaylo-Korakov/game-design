using Assets.IntLibs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2.5f;
    [SerializeField] float jumpSpeed = 5.0f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnJump()
    {
        //if (!context.performed)
        //    return;

        // Debug.Log("TEST: " + context.ToString());

        Vector3 movement = new Vector3(0.0f, 10.0f, 0.0f);

        rb.AddForce(movement * this.jumpSpeed);
    }

    protected Vector3 CalculateMoveDirection()
    {
        var mainCamera = Camera.main.transform;
        Vector3 cameraForward = new(mainCamera.forward.x, 0, mainCamera.forward.z);
        Vector3 cameraRight = new(mainCamera.right.x, 0, mainCamera.right.z);

        Vector3 moveDirection = cameraForward.normalized * movementY + cameraRight.normalized * movementX;
        
        return moveDirection;
    }


    private void FixedUpdate()
    {
        Vector3 movement = CalculateMoveDirection();
        rb.AddForce(movement * this.movementSpeed);
    }
}
