using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 6.0f;
    public float rotationSpeed = 4.0f;
    public float runningSpeed = 12.0f;
    public float gravity = -9.8f;

    private float verticalVelocity;
    private float jumpForce = 15.0f;
    private float gravityJump = 14.0f;

    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float movementSpeed = speed;

        // Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = runningSpeed;
        }

        float deltaX = Input.GetAxis("Horizontal") * rotationSpeed;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, movementSpeed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);

        if (charController.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity = -gravityJump * Time.deltaTime;
            }
        }
        else
        {
            verticalVelocity -= gravityJump * Time.deltaTime;
        }

        // Rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        charController.Move(jumpVector * Time.deltaTime);

    }
}