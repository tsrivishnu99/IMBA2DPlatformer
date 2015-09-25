using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (this.name == "LevelCompleteCollectible")
        {
            ScoreManager.score -= Time.time;
            LevelManager.levelComplete = true;
        }
        else if (other.tag == "Player")
        {
            ScoreManager.score += 10;
            Destroy(this.gameObject);
        }
    }
}
