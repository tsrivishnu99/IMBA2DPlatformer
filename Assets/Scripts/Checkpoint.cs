using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<player_InputHandler_v3>().startPos = this.transform.position;
        }
    }
}
