using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float sprintSpeedMultiplier = 2.0f;
    [SerializeField]
    float gravity = 9.8f;
    [SerializeField]
    float maxVelocityChange = 10.0f;
    [SerializeField]
    bool canJump = true;
    [SerializeField]
    float jumpHeight = 2.0f;

    bool grounded = false;
    Rigidbody rb;
    float sprintSpeed;
    float sprintSpeedRegular = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            if (Input.GetButton("Sprint"))
            {
                sprintSpeed = sprintSpeedMultiplier;
            }
            else
            {
                sprintSpeed = sprintSpeedRegular;
            }

            HandleMovement();

            // Jump
            /*if (canJump && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(rb.velocity.x, CalculateJumpVerticalSpeed(), rb.velocity.z);
            }*/
        }

        if (!grounded)
        {
            HandleMovement();
        }

        // Apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

        grounded = false;
    }

    void HandleMovement()
    {
        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed * sprintSpeed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
