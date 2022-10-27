using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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

        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        //print(playerInput.Player.MouseValue.ReadValue<Vector2>());
        
        transform.RotateAround(transform.position, new Vector3(0, 1, 0), currentMouseValue.x * sensibility * Time.fixedDeltaTime);
        print(playerHead.transform.localRotation.x);
        if (playerHead.transform.localRotation.x <= 90 && playerHead.transform.localRotation.x >= -90)
        {
            playerHead.transform.Rotate(Vector3.left, currentMouseValue.y * (sensibility / 1.5f) * Time.fixedDeltaTime);
        } else
        {
            playerHead.transform.Rotate(Vector3.left, 0f);
        }

        if (isWalking)
        {
            rb.velocity += transform.forward * speed * Time.fixedDeltaTime * currentSpeedValue;
        }

        if (isSidedWalking)
        {
            rb.velocity += transform.right * sidedSpeed * Time.fixedDeltaTime * currentSidedSpeedValue;
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

    void GetValueMouse(InputAction.CallbackContext context)
    {
        currentMouseValue = context.ReadValue<Vector2>();
        print(currentMouseValue.y);
    }
}
