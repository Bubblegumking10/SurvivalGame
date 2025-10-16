using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;               // Movement speed
    public float gravity = -9.81f;         // Gravity strength
    public float jumpHeight = 1.5f;        // Jump strength
    public Transform groundCheck;          // Empty object under the player to detect ground
    public float groundDistance = 0.4f;    // Radius for ground check
    public LayerMask groundMask;           // Define what layers count as ground
    private Animator animator;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
         animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isAiming = Input.GetMouseButton(1); // Right mouse button

        // --- Ground Check ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // keep grounded

        // --- Movement Input ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // --- Jump ---
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // --- Apply Gravity ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 moveDirection = transform.forward * moveZ;

        if (move != Vector3.zero)
        {
            // Move the player
            controller.Move(moveDirection * (isRunning ? runSpeed : walkSpeed) * Time.deltaTime);

            // Trigger walking or running animation
            if (isRunning)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            // Idle
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        // Handle aiming
        animator.SetBool("isAiming", isAiming);

        // Apply gravity
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0f;
        }
    }
}

