using UnityEngine;
using System.Collections;

public class SpikeObstacle : MonoBehaviour {
    GameObject player;
    Vector3 startPosition;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.transform.position = startPosition;
        }
    }
}
