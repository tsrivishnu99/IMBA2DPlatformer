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
	void Update () {

        if (Input.GetKeyDown(KeyCode.Z))
            sticky();

        if (Input.GetKeyDown(KeyCode.X))
            bouncy();

        if (Input.GetKeyDown(KeyCode.C))
            heavy();

        if (Input.GetKeyDown(KeyCode.V))
            setFlight();

        if(flight)
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
        playerCollider.material = stickyMaterial;
        this.GetComponent<playerController_v2>().jumpHeight = 1.0f;
        
    }

    void bouncy()
    {
        setDefault();
        playerCollider.material = bouncyMaterial;
    }

    void setDefault()
    {
        playerCollider.material = defaultMaterial;
        this.GetComponent<playerController_v2>().jumpHeight = 2.0f;
        this.GetComponent<Rigidbody>().mass = 10.0f;
        this.GetComponent<playerController_v2>().gravity = 10.0f;
        this.GetComponent<playerController_v2>().speed = 10.0f;
        this.GetComponent<playerController_v2>().boost = 0.1f;
        this.GetComponent<playerController_v2>().maxVelocityChange = 5.0f;
        flight = false;
    }

    void heavy()
    {
        setDefault();
        playerCollider.material = heavyMaterial;
        this.GetComponent<playerController_v2>().jumpHeight = 1.0f;
        this.GetComponent<Rigidbody>().mass = 20.0f;
    }

    void setFlight()
    {
        setDefault();
        this.GetComponent<playerController_v2>().gravity = 1.0f;
        this.GetComponent<playerController_v2>().speed = 0.1f;
        this.GetComponent<playerController_v2>().boost = 0.05f;
        this.GetComponent<playerController_v2>().maxVelocityChange = 1.0f;
        flight = true;
    }
}
