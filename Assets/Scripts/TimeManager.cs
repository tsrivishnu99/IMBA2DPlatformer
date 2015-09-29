using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    Text timeText;
    public static float remainingTime;
	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
        remainingTime = 10000.0f;
	}
	
	// Update is called once per frame
	void Update () {
        remainingTime -= Time.deltaTime;
        timeText.text = "Timer: " + (int) remainingTime;
        if (remainingTime <= 0.0f)
            GameObject.FindGameObjectWithTag("Player").GetComponent<player_InputHandler_v3>().ResetPosis();
	}
}
