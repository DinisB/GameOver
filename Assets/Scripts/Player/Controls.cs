using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    private float externalJump = 0f;

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
        controller = gameObject.GetComponent<CharacterController>();
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

        input = canMove ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        Vector3 move = cam.transform.forward * input.y + cam.transform.right * input.x;
        move.y = 0;
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
            transform.forward = move;

        if (groundedPlayer) playerVelocity.y = 0;

        Vector3 horizontalMove = move * playerSpeed;

        // Salto normal / salto externo
        if (jumpAction.action.triggered && groundedPlayer && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }
        else if (externalJump > 0f)
        {
            playerVelocity.y = externalJump;
            externalJump = 0f;
        }


        // gravidade
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Movimento final
        Vector3 finalMove = horizontalMove + playerVelocity.y * Vector3.up;
        controller.Move(finalMove * Time.deltaTime);
    }
    public bool IsGrounded()
    {
        return groundedPlayer;
    }
    public void JumpFromExternal(float jumpForce)
    {
        externalJump = jumpForce;
    }
}


