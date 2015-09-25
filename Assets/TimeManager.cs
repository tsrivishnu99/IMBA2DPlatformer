using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    Text timeText;
    public static float remainingTime;
	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
        remainingTime = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
        remainingTime -= Time.deltaTime;
        timeText.text = "Timer: " + (int) remainingTime;
        if (remainingTime <= 0.0f)
            Application.LoadLevel(Application.loadedLevel);
	}
}
