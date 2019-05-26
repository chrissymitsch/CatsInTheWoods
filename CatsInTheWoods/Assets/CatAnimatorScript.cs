using UnityEngine;

public class CatAnimatorScript : MonoBehaviour
{
    public float speed = 6.0f;
    //public float rotationSpeed = 4.0f;
    public float runningSpeed = 12.0f;
    public float gravity = -9.8f;
    public float mouseSensibilitaet = 10;
    public bool vehicleActive = false;

    private float animatorSpeed;
    private float verticalVelocity;
    private float jumpForce = 15.0f;
    private float gravityJump = 14.0f;

    private CharacterController charController;
    private Animator animator;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animatorSpeed = animator.speed;
    }

    void Update()
    {
        if (!vehicleActive)
        {
            float movementSpeed = speed;
            bool running = false;
            bool walking = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = runningSpeed;
                running = true;
                walking = false;
            }

            float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
            float deltaZ = Input.GetAxis("Vertical") * movementSpeed;

            Vector3 movement = new Vector3(deltaX, 0, deltaZ);

            movement = Vector3.ClampMagnitude(movement, movementSpeed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            animator.SetFloat("Speed", movement.magnitude);
            charController.Move(movement);

            // Einfrieren der Jump-Animation aufheben
            animator.speed = animatorSpeed;
            animator.SetBool("isFalling", false);

            if (charController.isGrounded)
            {
                verticalVelocity = -gravityJump * Time.deltaTime;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
                    || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    animator.SetBool("isWalking", walking);
                    animator.SetBool("isRunning", running);
                    animator.SetBool("isIdle", false);
                }
                else
                {
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isIdle", true);
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    verticalVelocity = jumpForce;
                    animator.SetTrigger("isJumping");
                }
            }
            else
            {
                verticalVelocity -= gravityJump * Time.deltaTime;
                animator.SetBool("isFalling", true);
                // Wenn fallend, friert Jump-Animation nach einer bestimmten Zeit ein
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f)
                {
                    animator.speed = 0;
                }
            }

            // Rotation
            //float horizontalRotation = deltaX;
            if (Input.GetMouseButton(1))
            {
                float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensibilitaet;
                transform.Rotate(0, horizontalRotation, 0);
            }
            //transform.Rotate(0, horizontalRotation, 0);

            Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
            charController.Move(jumpVector * Time.deltaTime);
        }
    }
}