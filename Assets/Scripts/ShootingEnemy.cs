using UnityEngine;
using System.Collections;

public class ShootingEnemy : MonoBehaviour
{
    public Transform center;
    public float degreesPerSecond = -65.0f;
    public Transform bullet;
    public GameObject bulletSpawn;

    private Vector3 vec;

    // Use this for initialization
    void Start()
    {
        vec = transform.position - center.position;
        InvokeRepeating("Shoot", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        vec = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * vec;
        transform.position = center.position + vec;
    }

    void Shoot()
    {
        Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
    }
}
