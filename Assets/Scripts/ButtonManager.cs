using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject leaderBoard;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer cloud;
    public Leaderboard leaderboard;
    private Wallspike lWallspikes;
    private Wallspike rWallspikes;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        //Set Game object inactive so game doesn't start till button pressed
        character.SetActive(false);
        scoreUI.SetActive(false);
        lWallspikes = GameObject.Find("Left Wallspikes").GetComponent<Wallspike>();
        rWallspikes = GameObject.Find("Right Wallspikes").GetComponent<Wallspike>();
    }
    public void StartButtonPressed()
    {
        //Start game after button press
        mainMenu.SetActive(false);
        StartCoroutine(StartGame());
        lWallspikes.Shuffle();
        rWallspikes.Shuffle();
        audioSource.PlayOneShot(clip);
    }

    IEnumerator StartGame()
    {
        //Wait 3 seconds before game starts
        playerMovement.ResetScore();
        scoreUI.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Game Started");
        character.SetActive(true);           
    }

    public void GameOver()
    {
        //Game Over UI Panel
        StartCoroutine(leaderboard.SubmitScoreRoutine((int)playerMovement.score));
        gameOver.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        //Returns to Main Menu Button
        gameOver.SetActive(false);
        mainMenu.SetActive(true);
        audioSource.PlayOneShot(clip);
    }

    public void PlayAgain()
    {
        //Play Again button
        gameOver.SetActive(false);
        StartCoroutine(StartGame());
        lWallspikes.Shuffle();
        rWallspikes.Shuffle();
        audioSource.PlayOneShot(clip);
    }

    public void OpenLeaderboard()
    {
        //Opens Leaderboard & Gets Leaderboards Scores
        mainMenu.SetActive(false);
        StartCoroutine(leaderboard.FetchTopHighscoresRoutine());
        leaderBoard.SetActive(true);
        audioSource.PlayOneShot(clip);
    }

    public void CloseLeaderBoard()
    {
        //Closes Leaderboard & Returns Main Menu
        mainMenu.SetActive(true);
        leaderBoard.SetActive(false);
        audioSource.PlayOneShot(clip);
    }
}
