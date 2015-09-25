using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]

public class playerController_v2 : MonoBehaviour
{
    public float speed;
    public float gravity = 10.0f;
    public float maxVelocityChange = 5.0f;
    public float boost ;
    public bool canJump = true;
    public float jumpHeight;
    public float horizontalPush;
    public float rotationSpeed = 2.0f;
    public bool grounded = false;
    public float groundPosis;

    public bool isFlightPrev = false;
    public bool isFlightMode = false;

    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
       // rigidbody.useGravity = false;
    }
    void Start()
    {
        groundPosis = this.transform.position.y;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            groundPosis = this.transform.position.y;
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);            // Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity 
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.z = 0;   
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButtonDown("Jump"))
            {
                grounded = false;
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

        if(isFlightMode)
        {
            rigidbody.velocity = new Vector3(0f, 2f, 0f);
        }
        
        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        grounded = false;
        if (this.transform.position.y >= groundPosis + 3.0f && !isFlightMode &&!isFlightPrev)
        {

         this.transform.position =  new Vector3 (this.transform.position.x, Mathf.Clamp(this.transform.position.y, groundPosis, groundPosis + 3.0f) , this.transform.position.z);

        }
       
    }

    void OnCollisionStay(Collision collisionInf)
    {
        if (collisionInf.collider.name.Contains("Platform"))
        {
            grounded = true;
         
        }

    }
    void OnCollisionEnter(Collision collisionInf)
    {

        RaycastHit hit;

        Physics.Raycast(this.transform.position, new Vector3(Input.GetAxis("Horizontal"), 0f, 0f), out hit, 1f);
        Debug.DrawRay(this.transform.position, new Vector3(Input.GetAxis("Horizontal"), 0f, 0f), Color.blue);

        float angle = Vector3.Angle(hit.normal, new Vector3(Input.GetAxis("Horizontal"), 0f, 0f));
        //Debug.Log(angle + "" + hit.normal + "" + Input.GetAxis("Horizontal"));


        if (angle == 180)
        {
            Vector3 velocity = rigidbody.velocity;
            rigidbody.velocity = new Vector3(CalculateJumpHorizontalSpeed(), velocity.y, velocity.z);

        }
        else if (collisionInf.collider.name.Contains("Platform"))
        {
            grounded = true;
            isFlightPrev = false;
        }
        
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    float CalculateJumpHorizontalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * horizontalPush * gravity);
    }
}