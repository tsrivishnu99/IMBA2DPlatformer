using UnityEngine;
using System.Collections;

public class BreakablePlatform : MonoBehaviour {
    private Vector3 initialVelo;
    void OnCollisionEnter(Collision other)
    {
        initialVelo = other.rigidbody.velocity;
        if (other.gameObject.tag == "Player" && this.tag == "Breakable" && other.gameObject.GetComponent<playerController_v2>().isHeavyMode)
        {
            Destroy(this.gameObject);
            other.rigidbody.velocity = initialVelo;
        }
    }
}
