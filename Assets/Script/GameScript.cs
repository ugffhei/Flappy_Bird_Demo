using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    //======================= Game States
    //Defines if the game is performing automatic action or player is in control 
    private enum GameState
    {
        WaitingToClick,   //Player has to click to start game 
        Playing,          //Play can fly up and pipes are spawning 
        LostMenu,         //Lost Menu Buttons 
    }
    private GameState _currentGame = GameState.WaitingToClick;     //Current state of the game 

    //======================= Gameplay Controllers 
    //
    private Bird bird;
    private PipeSpawner pipeSpawner;
    private Data data;

    //======================= UI Controller 
    //
    private GameObject scoreImage;
    private GameObject clickToPlayText;
    private GameObject loseMenu;

    //======================= Text 
    private TextMeshProUGUI score;
    private TextMeshProUGUI best;

    //======================= Medal
    public Sprite silver;
    public Sprite gold;
    private Image medal;

    //====================== Audio
    private AudioSource button;

    // Start is called before the first frame update
    void Start()
    {
        //Gameplay Controllers 
        bird = GameObject.Find("FlappyBird").GetComponent<Bird>();
        pipeSpawner = GameObject.
            Find("PipeSpawner").GetComponent<PipeSpawner>();
        data = GameObject.Find("Data").GetComponent<Data>();

        //UI Controllers
        scoreImage = GameObject.Find("Canvas").transform.Find("ScoreText").gameObject;
        scoreImage.SetActive(true);

        clickToPlayText = GameObject.Find("Canvas").transform.Find("ClickToPlay").gameObject;
        clickToPlayText.SetActive(true);

        loseMenu = GameObject.Find("Canvas").transform.Find("LoseMenu").gameObject;
        loseMenu.SetActive(false);

        //Medal 
        medal = GameObject.Find("Canvas").transform.Find("LoseMenu").transform.Find("Medal").GetComponent<Image>();

        //Text 
        score = GameObject.Find("Canvas").transform.Find("LoseMenu").transform.Find("Score").GetComponent<TextMeshProUGUI>();
        best = GameObject.Find("Canvas").
            transform.Find("LoseMenu").transform
            .Find("Best").GetComponent<TextMeshProUGUI>();

        //Auido
        button = GameObject.Find("ButtonSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentGame)
        {
            case GameState.WaitingToClick:
                WaitingToClick();
                break;
            case GameState.Playing:
                UpdatePlaying();
                break;
            case GameState.LostMenu:

                break;
        }
    }

    //======================= States 

    //Wait for the player to start the game 
    private void WaitingToClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FromClickToPlay();
        }
    }

    //Update the player interaction with bird 
    //Updates the spawning of new pipes 
    private void UpdatePlaying()
    {
        bird.UpdateBird();
        pipeSpawner.UpdatePipeSpawner();
    }

    //============== Switching Between States

    //Called when player first clicks on screen 
    //Changes bird to use physics and animate 
    //Turns off the click animation 
    private void FromClickToPlay()
    {
        bird.SetUpBird();
        clickToPlayText.SetActive(false);
        _currentGame = GameState.Playing;
    }

    //Called from Bird, takes in the current score 
    //Turns off the number on top of the screen and turns on
    //The Game Over UI
    //Set the State to be Lost Menu
    public void FromPlayToLose(int counter)
    {
        //Checks if the new score is a high score 
        if (data.GetScore() < counter)
        {
            data.SetScore(counter);
        }

        //Changes medal sprite based on how high the score is
        if(counter >= 10 && counter < 20)
        {
            medal.sprite = silver;
        }
        else if(counter >= 20)
        {
            medal.sprite = gold;
        }

        //Updates the text 
        score.SetText(counter.ToString());
        best.SetText(data.GetScore().ToString());

        //Turns UI on/off
        scoreImage.SetActive(false);
        loseMenu.SetActive(true);

        //Moves to a diffrent state 
        _currentGame = GameState.LostMenu;
    }


    //============== Buttons 

    //Starts the process of restarting this Scene using button 
    public void Replay()
    {
        StartCoroutine(PlayAndLeave("GameScene"));
    }

    //Starts the process of moving to Menu Scene using button
    public void Quit()
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
