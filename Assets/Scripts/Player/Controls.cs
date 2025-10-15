using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    [SerializeField] private bool canJump;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    private GameObject cam;
    private bool canMove = true;
    private Vector2 input;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = transform.GetChild(0).gameObject;
    }

    private void Awake()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }


    public void ChangeMovementSpecific(bool x)
    {
        canMove = x;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        if (canMove)
        {
            input = moveAction.action.ReadValue<Vector2>();
        }
        else
        {
            input = Vector2.zero;
        }

        Vector3 move = camForward * input.y + camRight * input.x;
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        if (jumpAction.action.triggered && groundedPlayer && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        gameObject.transform.forward = camForward;
        controller.Move(finalMove * Time.deltaTime);
    }
}
