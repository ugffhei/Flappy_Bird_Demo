using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //Holds the text component that we'll assing current high score
    private TextMeshProUGUI highscore;
    //Data that stores the high score
    private Data data;
    //Button SFX 
    private AudioSource button;

    // Start is called before the first frame update
    void Start()
    {
        //Data to know what's the current high score 
        data = GameObject.Find("Data").GetComponent<Data>();
        //Connection to the Score 
        highscore = GameObject.Find("Canvas").transform.Find("Score").GetComponent<TextMeshProUGUI>();
        highscore.text = data.GetScore().ToString();

        //Getting audio for the button 
        button = GameObject.Find("ButtonSFX").GetComponent<AudioSource>();
    }

    //Goes to Game Scene
    public void ToLevel()
    {
        StartCoroutine(PlayAndLeave("GameScene"));
    }

    //Goes to Credit Scene 
    public void ToCredits()
    {
        StartCoroutine(PlayAndLeave("CreditsScene"));
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
