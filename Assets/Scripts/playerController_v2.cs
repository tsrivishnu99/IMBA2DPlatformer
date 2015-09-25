using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]

public class playerController_v2 : MonoBehaviour
{
    public float speed = 2.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 5.0f;
    public float boost = 2.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 2.0f;
    public bool grounded = false;

    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);            // Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = 0;   
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }
        else
        {
            if(this.gameObject.GetComponent<player_InputHandler_v3>().leftClamped)
            {
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);            // Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= boost;

                Vector3 velocity = rigidbody.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = 0;
                velocityChange.y = 0;

                rigidbody.AddForce(velocityChange, ForceMode.Impulse);
            }
            else
            {
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);            // Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= speed;

                Vector3 velocity = rigidbody.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = 0;
                velocityChange.y = 0;

                rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }
        
        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        grounded = false;
    }

    void OnCollisionStay(Collision collisionInf)
    {
        if(collisionInf.collider.name.Contains("Platform"))
             grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}