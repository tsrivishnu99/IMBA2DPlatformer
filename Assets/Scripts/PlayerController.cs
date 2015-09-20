using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public float playerMovementSpeed;
    public float playerJumpSpeed;

    private bool playerIsGrounded;

    private float moveHorizontal = 0.0f;
    private float moveVertical = 0.0f;

    private Rigidbody playerRigidBody;

	void Start () 
    {
        playerIsGrounded = true;
        playerRigidBody = GetComponent<Rigidbody>();	
	}
	
	void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        
        if(Input.GetButtonDown("Jump") && playerIsGrounded)
        {
            moveVertical = 1.0f;
            playerIsGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 force = new Vector3(moveHorizontal * playerMovementSpeed, moveVertical * playerJumpSpeed, 0.0f);
        playerRigidBody.AddForce(force);

        if (!playerIsGrounded)
            moveVertical = 0.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Platform")
            playerIsGrounded = true;
    }

}
