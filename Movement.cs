using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 2f;
    public Transform cam;
    private Animator animator;
    private bool isGrounded;
    private float gravity = -9.81f;
    private float jumpHeight = 1.5f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Lock cursor for better user experience
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMovement();
        // Other methods like animations, etc., can be called here
    }

    void PlayerMovement()
    {
        // Check if the character is grounded
        isGrounded = controller.isGrounded;

        // Get movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to camera
        Vector3 moveDirection = (cam.forward * moveVertical + cam.right * moveHorizontal).normalized;

        // Apply movement speed
        Vector3 moveVelocity = moveDirection * speed;

        // Apply gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensures the character sticks to ground when grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Combine horizontal and vertical movement with gravity
        Vector3 movement = moveVelocity + velocity;

        // Move the character
        controller.Move(movement * Time.deltaTime);

        // Optionally, you can add animations here
    }
}