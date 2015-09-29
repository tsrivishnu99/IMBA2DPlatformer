using UnityEngine;
using System.Collections;

public class SpikeObstacle : MonoBehaviour {
    GameObject player;
    Vector3 startPosition;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter(Collision other)
    { 
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<player_InputHandler_v3>().ResetPosis();
        }
    }
}
