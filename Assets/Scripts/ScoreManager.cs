using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static float score;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    scoreText.text = "Score: " + score;
	}
}
