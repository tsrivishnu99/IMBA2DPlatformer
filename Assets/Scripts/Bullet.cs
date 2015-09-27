using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public Rigidbody rigidBody;
    public float bulletSpeed;
    public Vector3 startPosition;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(-this.transform.right * bulletSpeed);
        startPosition = this.transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, startPosition)>= 200)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
