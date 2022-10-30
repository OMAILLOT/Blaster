using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed;
    [SerializeField] private float sidedSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sensibility;
    [SerializeField] GameObject playerHead;
    [SerializeField] private Rigidbody rb;
   
    private PlayerInput playerInput;

    private float currentSpeedValue;
    private float currentSidedSpeedValue;
    private Vector2 currentMouseValue;
    private bool isWalking;
    private bool isSidedWalking;
    private bool isJumping;

    private float baseSpeed;
    private float baseSidedSpeed;
    private bool isGrounded;
    private float baseDrag;



    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Walk.started += OnWalkPressed;
        playerInput.Player.Walk.canceled += OnWalkPressed;

        playerInput.Player.SidedWalk.started += OnSidedWalkPressed;
        playerInput.Player.SidedWalk.canceled += OnSidedWalkPressed;

        playerInput.Player.MouseValue.started += GetValueMouse;
        playerInput.Player.MouseValue.canceled += GetValueMouse;
        playerInput.Player.MouseValue.performed += GetValueMouse;

        playerInput.Player.Jump.started += OnJumpPressed;
        playerInput.Player.Jump.canceled += OnJumpPressed;

        Cursor.lockState = CursorLockMode.Locked;

        baseSpeed = speed;
        baseSidedSpeed = sidedSpeed;
        baseDrag = rb.drag;
    }

    void FixedUpdate()
    {        

        transform.RotateAround(transform.position, new Vector3(0, 1, 0), currentMouseValue.x * sensibility * Time.fixedDeltaTime);
        print(playerHead.transform.localRotation.x * 100);
        playerHead.transform.Rotate(Vector3.left, currentMouseValue.y * (sensibility / 1.5f) * Time.fixedDeltaTime,Space.Self);

        if (isWalking)
        {
            rb.velocity += transform.forward * speed * Time.fixedDeltaTime * currentSpeedValue;
        }

        if (isSidedWalking)
        {
            rb.velocity += transform.right * sidedSpeed * Time.fixedDeltaTime * currentSidedSpeedValue;
        }


        bool hit = Physics.Raycast(transform.position, Vector3.down, 1.2f, groundLayerMask);
        isGrounded = hit;

            if (hit)
            {
                rb.drag = baseDrag;
                speed = baseSpeed;
                sidedSpeed = baseSidedSpeed;
            } else
            {
                rb.drag = 0;
                speed = baseSpeed / 5;
                sidedSpeed = baseSidedSpeed / 5;
            }
    }


    void OnWalkPressed(InputAction.CallbackContext context)
    {
        currentSpeedValue = context.ReadValue<float>() * 2;
        if (currentSpeedValue >= 1) currentSpeedValue = 1;
        if (currentSpeedValue <= -1) currentSpeedValue = -1;

        isWalking = currentSpeedValue != 0;
    }

    void OnSidedWalkPressed(InputAction.CallbackContext context)
    {
        currentSidedSpeedValue = context.ReadValue<float>() * 2;
        if (currentSidedSpeedValue >= 1) currentSidedSpeedValue = 1;
        if (currentSidedSpeedValue <= -1) currentSidedSpeedValue = -1;

        isSidedWalking = currentSidedSpeedValue != 0;
    }

    void OnJumpPressed(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValue<float>() > 0;
        if (isJumping && isGrounded)
        {
            rb.AddForce(0, jumpForce * 1000, 0);
        }
    }
    void GetValueMouse(InputAction.CallbackContext context)
    {

            currentMouseValue = context.ReadValue<Vector2>();
        if (playerHead.transform.localRotation.x * 100 >= 70 &&
            currentMouseValue.y > 0)
        {
            currentMouseValue.y = 0f;
            if (playerHead.transform.localRotation.x * 100 >= 80) playerHead.transform.Rotate(Vector3.right, 69.9f);
        }

        if (playerHead.transform.localRotation.x * 100 <= -70 &&
            currentMouseValue.y < 0)
        {
            currentMouseValue.y = 0f;
            if (playerHead.transform.localRotation.x * 100 <= -80) playerHead.transform.Rotate(Vector3.right, -69.9f);


        }

    }
}
