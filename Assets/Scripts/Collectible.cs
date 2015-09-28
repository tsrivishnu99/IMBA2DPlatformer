using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    private Vector3 initialVelo;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.name == "LevelCompleteCollectible")
            {
                ScoreManager.score += TimeManager.remainingTime;
                LevelManager.levelComplete = true;
                Destroy(this.gameObject);
            }
            else if (this.tag == "Collectible")
            {
                ScoreManager.score += 10;
                Destroy(this.gameObject);
            }
        }
    }
}
