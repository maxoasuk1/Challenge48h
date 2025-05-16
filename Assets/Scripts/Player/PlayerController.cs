using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public float jumpForce;
    public float airMultiplier;

    [Header("Ground Check")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    bool grounded;

    Rigidbody rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump
        if (Input.GetKeyDown(jumpKey) && grounded)
        {
            Jump();
        }

        MovePlayer();
        SpeedControl();
        UpdateAnimation();

        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.05f, whatIsGround);
        rb.linearDamping = grounded ? groundDrag : 0f;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void UpdateAnimation()
    {
        // On utilise la vitesse de déplacement comme paramètre pour l'Animator
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float speed = flatVel.magnitude;

        animator.SetFloat("Speed", speed);
    }
}
