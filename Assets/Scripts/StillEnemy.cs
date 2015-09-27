using UnityEngine;
using System.Collections;

public class StillEnemy : MonoBehaviour
{
    public Transform bullet;
    public GameObject bulletSpawn;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Shoot", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
       this.transform.LookAt(player.transform.position);
    }

    void Shoot()
    {
        Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
    }
}
