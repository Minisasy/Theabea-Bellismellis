using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    //private ThirdPersonActionsAsset playerActionsAsset;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    [SerializeField] string actionMapName;
    private InputAction move;

    private Rigidbody rb;
    [SerializeField] float movementForce = 1f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] GameObject playerObject;
    private bool grounded = false;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] Camera playerCamera;
    private Animator animator;
    private BoxCollider boxColl;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        //playerActionsAsset = new ThirdPersonActionsAsset();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap(actionMapName);
        animator = this.GetComponent<Animator>();
        boxColl = this.GetComponent<BoxCollider>();
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        /*playerActionsAsset.Player.Jump.started += DoJump;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();*/
        player.FindAction("Jump").started += DoJump;
        move = player.FindAction("Move");
        player.Enable();
    }

    private void OnDisable()
    {
        /*playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Disable();*/
        player.FindAction("Jump").started -= DoJump;
        player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        //makes no floatiness
        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        //capping velocity
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        if (grounded == true)
        {
            forceDirection += Vector3.up * jumpForce;
            animator.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Ground")
        {
            grounded = true;
            Debug.Log("true");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }
    public void PlayerRespawn(Vector3 respawnPoint)
    {
        this.transform.position = respawnPoint;
    }
}
