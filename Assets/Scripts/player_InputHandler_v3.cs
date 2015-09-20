using UnityEngine;
using System.Collections;

public class player_InputHandler_v3 : MonoBehaviour {

	private Rigidbody player;
   	public bool leftClamped;
	private Vector3 leftHitPoint;
	private Vector3 startPos;
	public Camera fpsCam;
	RaycastHit hit;
	public GameObject RopeElement;
	private GameObject[] Rope;
	
	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody>();
		leftClamped = false;
		//rightClamped = false;
		leftHitPoint.x = 0;
		leftHitPoint.y = 0;
		leftHitPoint.z = 0;
		startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Cursor.lockState = CursorLockMode.Locked;

        //Debug.Log(playerNumber.ToString());
		// Handle left button click, [ Find the point of collision and store the point ]
		if (Input.GetMouseButton(0))
        {
            //Debug.Log("Fire1 was pressed");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray = new Ray(player.transform.position, );

			if (Physics.Raycast(ray, out hit) && leftClamped == false && hit.collider.name.Contains("Platform") && this.gameObject.GetComponent<playerController_v2>().grounded == false)
			{
				leftHitPoint = hit.point - transform.position;
				leftHitPoint.Normalize();
				//player.AddForce(leftHitPoint * 5.0f, ForceMode.Impulse);
				//leftHitPoint = hit.point;
				
				//HingeJoint HJ;
				ConfigurableJoint HJ;
				
				float ropeLength = Vector3.Distance(hit.point, transform.position);

				int totalRopeUnits = (int)(ropeLength / 0.2f);
				float distanceBetween = 0.2f;

				while (totalRopeUnits > 30)
				{
					distanceBetween += 0.2f;
					totalRopeUnits = (int)(ropeLength / distanceBetween);
				}

				leftClamped = true;
				Rope = new GameObject[totalRopeUnits + 1];
				
				GameObject ropeUnit, LastUnit;
				
				ropeUnit = Instantiate(RopeElement);
				ropeUnit.transform.position = hit.point;
				ropeUnit.GetComponent<Rigidbody>().isKinematic = true;
				//ropeUnit.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
				LastUnit = ropeUnit;
				Rope[0] = ropeUnit;
				
				for (int i = 1; i < totalRopeUnits ; i++)
				{
					ropeUnit = Instantiate(RopeElement);
					ropeUnit.name = i.ToString();
					ropeUnit.transform.position = hit.point - (leftHitPoint * distanceBetween * i);
					//ropeUnit.transform.rotation = new Quaternion(0.0f,0.0f,0.0f,0.0f);
					//HJ = ropeUnit.gameObject.GetComponent<HingeJoint>();
					HJ = ropeUnit.gameObject.GetComponent<ConfigurableJoint>();
					HJ.connectedBody = LastUnit.gameObject.GetComponent<Rigidbody>();
					ropeUnit.gameObject.GetComponent<Rigidbody>().mass = 2.0f;
					HJ.anchor = ropeUnit.transform.InverseTransformPoint(LastUnit.transform.position);
					
					LastUnit = ropeUnit;
					Rope[i] = ropeUnit;
				}
				
				this.gameObject.AddComponent<ConfigurableJoint>();
				HJ = this.gameObject.GetComponent<ConfigurableJoint>();
				HJ.connectedBody = LastUnit.gameObject.GetComponent<Rigidbody>();
				HJ.xMotion = ConfigurableJointMotion.Locked;
				HJ.yMotion = ConfigurableJointMotion.Locked;
				HJ.zMotion = ConfigurableJointMotion.Locked;
				//HJ.axis = new Vector3(1.0f,1.0f,1.0f);
				HJ.anchor = transform.InverseTransformPoint(LastUnit.transform.position);

			}
		}

     	//Release the clamps for left and right respectively
        if (Input.GetMouseButton(1) && leftClamped ==true)
		{
            CutRope();
		}
		
	}
	
	void FixedUpdate()
	{
		if (this.transform.position.y < -15.0f) 
		{
			this.transform.position = new Vector3 (startPos.x,startPos.y,startPos.z);
			this.gameObject.GetComponent<Rigidbody>().AddForce(-this.gameObject.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
            

			if(leftClamped)
			{
                CutRope();
			}
		}
		
	}

    void CutRope()
    {
        leftClamped = false;
        Destroy(this.gameObject.GetComponent<ConfigurableJoint>());
        for (int i = Rope.Length - 1; i >= 0; i--)
        {
            DestroyObject(Rope[i]);
        }
    }
}

