using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private bool canMove = true;
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Boost Config")]
    public float boostMult = 1;
    public LayerMask whatIsBoost;

    [Header("Finishline Config")]
    public LayerMask whatIsFinishLine;
    bool finished;


    [Header("Leftovers")]
    public Transform orientation;
    public MovementState state;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector3.zero; // Stop all motion
            return; // Skip movement logic
        }
        MyInput();
        StateHandler();
        SpeedControl(); // Seems to limit my velocity even while in the air
        Boost();
    }

    private void FixedUpdate()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.1f, whatIsGround);

        // Declare that when this raycast hits whatIsFinishLine, finish switches to true
        finished = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.2f, whatIsFinishLine);

        if (finished == true)
        {
            Finish();
        }

        MovePlayer();

        if (grounded)
            rb.linearDamping = groundDrag;

        else
            rb.linearDamping = 0;
    }

    private void Finish()
    {

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown); // Makes you able to continuously jump by holding down space
        }
    }

    private void StateHandler()
    {
        // Sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        // On ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // In air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * airMultiplier * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Boost()
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.2f, whatIsBoost))
        {
            rb.AddForce(transform.up * boostMult, ForceMode.Impulse);
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None; // Allow movement again
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}