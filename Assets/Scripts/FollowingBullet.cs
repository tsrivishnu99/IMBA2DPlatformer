using UnityEngine;
using System.Collections;

public class FollowingBullet : MonoBehaviour {

    public Rigidbody rigidBody;
    public float bulletSpeed;
    public Vector3 startPosition;

    public GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.LookAt(player.transform.position);
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(this.transform.forward * bulletSpeed);
        startPosition = this.transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, startPosition) >= 200)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
