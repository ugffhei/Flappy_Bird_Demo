using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bird : MonoBehaviour
{
    //================== Score 
    private TextMeshProUGUI scoreText;
    private int scoreCounter = 0;

    //================== Game Script For when we change States 
    private GameScript gameScript;

    //================== Controls How Bird Behaves 
    private Rigidbody2D rigidbody2D;
    public float forceScale = 10f;
    private BoxCollider2D boxCollider2D;

    //================== Animates or changes sprite to be dead 
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Sprite deathSprite;

    //================= SoundEffects
    private AudioSource pointSFX;
    private AudioSource dieSFX;

    // Start is called before the first frame update
    void Start()
    {
        //UI
        scoreText = GameObject.Find("Canvas").transform.Find("ScoreText")
            .GetComponent<TextMeshProUGUI>();
        scoreText.text = "" + scoreCounter;

        //Connection to Game Controller 
        gameScript = GameObject.
            Find("GameController").GetComponent<GameScript>();

        //Controlls how bird behaves 
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;

        //Controls sprite 
        spriteRenderer = transform.GetChild(0)
            .GetComponent<SpriteRenderer>();

        //Sets animation on and off
        animator = GetComponent<Animator>();
        animator.enabled = false;

        //Gets SFX 
        pointSFX = transform.GetChild(1).GetComponent<AudioSource>();
        dieSFX = transform.GetChild(2).GetComponent<AudioSource>();
    }

    //When the player starts the game gravity is set and 
    //Animation started 
    public void SetUpBird()
    {
        rigidbody2D.gravityScale = 0.4f;
        animator.enabled = true;
    }

    //Listens for the Player to click mouse to send Bird flying 
    public void UpdateBird()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rigidbody2D.velocity = forceScale * Vector2.up;
        }
    }

    //Checks if the bird connected with a pipe 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            //Let the bird fall faseter
            rigidbody2D.gravityScale = 1;
            //Let it fall off the screen 
            boxCollider2D.enabled = false;
            //Change the sprite to be gray 
            spriteRenderer.sprite = deathSprite;
            //Stop it from aniamting 
            animator.enabled = false;
            //Play the death SFX 
            dieSFX.Play();
            //Change the State of the game 
            gameScript.FromPlayToLose(scoreCounter);
        }
    }

    //Checks if the player passed the goal post then updates score 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            //Increase score 
            scoreCounter++;
            //Update UI 
            scoreText.text = "" + scoreCounter;
            //Play SFX 
            pointSFX.Play();
        }
    }
}
