using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]

public class playerController_v2 : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float boost = 0.1f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 2.0f;
    public bool grounded = false;

	public float bounceFactor;

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
            // Debug.Log(Input.GetAxis("Horizontal"));
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);            // Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = 0;   // Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
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
                rigidbody.AddForce(targetVelocity, ForceMode.Impulse);
            }
        }
        
        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        grounded = false;
    }


	void OnCollisionEnter(Collision collisionInf)
	{	
		//Debug.Log ("collisionEntered");
		ContactPoint contact = collisionInf.contacts[0];

		rigidbody.velocity = new Vector3(0, CalculateJumpVerticalSpeed(), 0);
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
		return Mathf.Sqrt(bounceFactor * 2 * jumpHeight * gravity);
    }
}