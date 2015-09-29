using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

    //public AudioClip start;
    //public AudioSource source;

    public void StartGame()
    {
        //   source.Play();	
        StartCoroutine("LoadMainLevel");
    }

    public void Tutorial()
    {
        //source.Play();	
        StartCoroutine("LoadTutorial");
    }

    public void Level2()
    {
        //source.Play();	
        StartCoroutine("LoadLevel2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadMainLevel()
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Tutorial");
    }

    private IEnumerator LoadTutorial()
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Tutorial");
    }

    private IEnumerator LoadLevel2()
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("level2");
    }

}
