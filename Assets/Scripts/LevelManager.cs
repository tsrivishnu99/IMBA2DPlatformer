using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Text levelCompleteText;
    public static bool levelComplete;
    private string levelCompleteMessage = "Level Complete!";
	// Use this for initialization
	void Start () {
        levelCompleteText = GetComponent<Text>();
        levelCompleteText.enabled = false;
        levelComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (levelComplete)
        {
            StartCoroutine(ShowLevelCompleteMessage(levelCompleteMessage, 2));
        }
	}

    IEnumerator ShowLevelCompleteMessage(string message, float delay)
    {
        levelCompleteText.text = message;
        levelCompleteText.enabled = true;
        yield return new WaitForSeconds(delay);
        levelCompleteText.enabled = false;
        levelComplete = false;
    }
}
