using UnityEngine;
using System.Collections;

public class ChangeState : MonoBehaviour {

    private SphereCollider playerCollider;
    public PhysicMaterial stickyMaterial;
    public PhysicMaterial bouncyMaterial;
    public PhysicMaterial defaultMaterial;
    public PhysicMaterial heavyMaterial;
    public float flight_acceleration;
    private bool flight;

	// Use this for initialization
	void Start () {
        flight = false;
	    playerCollider = this.GetComponent<SphereCollider>();
        setDefault();
	}
	
	// Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Z))
            sticky();

        if (Input.GetKeyDown(KeyCode.X))
            bouncy();

        if (Input.GetKeyDown(KeyCode.C))
            heavy();

        if (Input.GetKeyDown(KeyCode.V))
            setFlight();

        if (Input.GetKeyDown(KeyCode.Q))
            setDefault();

        if (flight)
        {
            if (Input.GetButton("Jump"))
            {
                this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, flight_acceleration, 0.0f), ForceMode.Force);
            }
        }
    }

    void sticky()
    {
        
        setDefault();
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 0);
        playerCollider.material = stickyMaterial;
        this.GetComponent<playerController_v2>().jumpHeight = 1.0f;
    }

    void bouncy()
    {
  
        setDefault();
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0);
        playerCollider.material = bouncyMaterial;
    }

    void setDefault()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 1, 0);
        playerCollider.material = defaultMaterial;
        this.GetComponent<playerController_v2>().jumpHeight = 5.0f;
        this.GetComponent<Rigidbody>().mass = 10.0f;
        this.GetComponent<playerController_v2>().gravity = 10.0f;
        this.GetComponent<playerController_v2>().speed = 10.0f;
        this.GetComponent<playerController_v2>().boost = 10.0f;
        this.GetComponent<playerController_v2>().maxVelocityChange = 5.0f;
        this.GetComponent<playerController_v2>().isFlightMode = false;
        flight = false;
    }

    void heavy()
    {
        
        setDefault();
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0);
        playerCollider.material = heavyMaterial;
        this.GetComponent<playerController_v2>().jumpHeight = 1.0f;
        this.GetComponent<Rigidbody>().mass = 20.0f;
    }

    void setFlight()
    {
       
        setDefault();
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
        this.GetComponent<playerController_v2>().gravity = 1.0f;
        this.GetComponent<playerController_v2>().speed = 0.0f;
        this.GetComponent<playerController_v2>().boost = 0.0f;
        this.GetComponent<playerController_v2>().maxVelocityChange = 0.0f;
        this.GetComponent<playerController_v2>().isFlightMode = true;
        flight = true;
        this.GetComponent<playerController_v2>().isFlightPrev = true;
    }
}
