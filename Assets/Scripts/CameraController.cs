using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public GameObject playerGameObject;

    public float cameraXOffset;

	// Update is called once per frame
	void LateUpdate () 
    {
        transform.position = new Vector3(playerGameObject.transform.position.x - cameraXOffset, playerGameObject.transform.position.y + 0.1f, transform.position.z);
	}
}
