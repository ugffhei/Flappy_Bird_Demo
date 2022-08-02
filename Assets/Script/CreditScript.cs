using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    //Gets the audio for the button SFX 
    private AudioSource button;

    // Start is called before the first frame update
    void Start()
    {
        //Getting audio for the button 
        button = GameObject.Find("ButtonSFX").GetComponent<AudioSource>();
    }

    //Starts the sequence to go to a diffrent scene
    public void ToLevel()
    {
        StartCoroutine(PlayAndLeave("MenuScene"));
    }


    //Waits until the button SFX has stopped playing and then goes to the
    //requested level
    private IEnumerator PlayAndLeave(string level)
    {
        button.Play();
        yield return new WaitForSeconds(button.clip.length);
        SceneManager.LoadScene(level);
    }
}
