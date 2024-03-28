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
    [SerializeField] float currentSpeed = 5f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] GameObject playerObject;
    private bool grounded = false;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] Camera playerCamera;
    private Animator animator;
    private BoxCollider boxColl;

    //Timer
    [SerializeField] ScriptableVariableSave variableSave;
    [SerializeField] bool red = false;
    bool timerOn = false;
    bool sprintOn = true;

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
        player.FindAction("Jump").started += DoJump;
        player.FindAction("Sprint").started += DoSprint;
        move = player.FindAction("Move");
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= DoJump;
        player.FindAction("Sprint").started -= DoSprint;
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
        if (horizontalVelocity.sqrMagnitude > currentSpeed * currentSpeed)
        {
            if (FindObjectOfType<Tutorial>() != null)
            {
                FindObjectOfType<Tutorial>().Walked(red);
            }
            rb.velocity = horizontalVelocity.normalized * currentSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
        Timer();
        SprintRecharge();
        if (red == true)
        {
            FindObjectOfType<GameController>().StaminaBarRed(sprintOn, timerOn);
        }
        if (red == false)
        {
            FindObjectOfType<GameController>().StaminaBarBlue(sprintOn, timerOn);
        }
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

    private void DoSprint(InputAction.CallbackContext context)
    {
        if (sprintOn == true)
        {
            if (FindObjectOfType<Tutorial>() != null)
            {
                FindObjectOfType<Tutorial>().Sprinted(red);
            }
            if (red == true)
            {
                Debug.Log("Sprint started");
                currentSpeed = sprintSpeed;
                variableSave.sprintRechargeRed = 0f;
                variableSave.timeLeftRed = 4f;
                timerOn = true;
            }
            if (red == false)
            {
                Debug.Log("Sprint started");
                currentSpeed = sprintSpeed;
                variableSave.sprintRechargeBlue = 0f;
                variableSave.timeLeftBlue = 4f;
                timerOn = true;
            }
        }
    }

    private void Timer()
    {
        if (timerOn)
        {
            if (red == true)
            {
                if (variableSave.timeLeftRed > 0)
                {
                    variableSave.timeLeftRed -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("sprint off");
                    currentSpeed = walkSpeed;
                    sprintOn = false;
                    variableSave.timeLeftRed = 0;
                    timerOn = false;
                }
            }
            if (red== false)
            {
                if (variableSave.timeLeftBlue > 0)
                {
                    variableSave.timeLeftBlue -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("sprint off");
                    currentSpeed = walkSpeed;
                    sprintOn = false;
                    variableSave.timeLeftBlue = 0;
                    timerOn = false;
                }
            }
        }
    }
    private void SprintRecharge()
    {
        if (!sprintOn)
        {
            if (red == true)
            {
                if (variableSave.sprintRechargeRed < 15f)
                {
                    variableSave.sprintRechargeRed += Time.deltaTime;
                }
                else
                {
                    sprintOn = true;
                    variableSave.timeLeftRed = 15f;
                }
            }
            if (red == false)
            {
                if (variableSave.sprintRechargeBlue < 15f)
                {
                    variableSave.sprintRechargeBlue += Time.deltaTime;
                }
                else
                {
                    sprintOn = true;
                    variableSave.timeLeftBlue = 15f;
                }
            }
        }
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        if (grounded == true)
        {
            if (FindObjectOfType<Tutorial>() != null)
            {
                FindObjectOfType<Tutorial>().Jumped(red);
            }
            forceDirection += Vector3.up * jumpForce;
            animator.SetTrigger("Jump");
            FindObjectOfType<AudioManager>().Play("Jump");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Ground")
        {
            grounded = true;
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
