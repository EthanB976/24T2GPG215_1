 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool Rightwall = false;

    public Leaderboard leaderboard;


    [SerializeField] private float highScore;

    [SerializeField] private TextMeshProUGUI highScoretxt;


    public float score;
    private float timer;
    private bool canDie = true;
    [SerializeField] private float iFrameTimer = 80f;


    [SerializeField] private TextMeshProUGUI scoretxt;

    [SerializeField] private ButtonManager buttonManager;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject startPostion;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse button pressed jump
        if (Input.GetMouseButtonDown(0))
        {
            rb2d.velocity = new Vector2(0, 5);
        }

        // if right wall is hit move left else move right
        if (Rightwall)
        {
            transform.Translate(Vector2.left * (3 * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector2.right * (3 * Time.deltaTime));
        }

        timer = timer - 1 * Time.deltaTime;
        if(timer < 0)
        {
            canDie = true;
        }
        else
        {
            canDie = false;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "rightwall")
        {
            timer = iFrameTimer;
            //Player turns left, Increase Score
            Rightwall = true;
            transform.localScale = new Vector3 (-0.8f, 0.8f, 0.8f);
            score += 1;
            scoretxt.text = score.ToString("F0");
            
        }

        else if (collision.tag == "leftwall")
        {
            timer = iFrameTimer;
            //Player turns right, Increase Score
            Rightwall = false;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            score += 1;
            scoretxt.text = score.ToString("F0");
            
        }

        else if (collision.tag == "spike" && canDie)
        {
            //Player died, Calls Highscore before restart game to save high score
            HighScore();
            RestartGame();
            StartCoroutine(leaderboard.SubmitScoreRoutine((int)score));
            gameObject.SetActive(false);
            buttonManager.GameOver();
        }

        else if (collision.tag == "killbarrier")
        {
            
            //Player died, Calls Highscore before restart game to save high score
            HighScore();
            RestartGame();
            StartCoroutine(leaderboard.SubmitScoreRoutine((int)score));
            gameObject.SetActive(false);
            buttonManager.GameOver();
        }
    }

    

    public void RestartGame()
    {
        //Reset player postion and make it face left
        Rightwall = false;
        player.transform.position = startPostion.transform.position;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void ResetScore()
    {
        //Resets Score UI
        score = 0;
        scoretxt.text = score.ToString("F0");
    }

    public void HighScore()
    {
        if(score >= highScore)
        {
            highScore = score;
            highScoretxt.text = "High Score: " + highScore.ToString("F0");
        }
    }
}
