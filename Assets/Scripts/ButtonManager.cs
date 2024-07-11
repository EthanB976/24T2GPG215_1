using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private PlayerMovement playerMovement;
    private Wallspike lWallspikes;
    private Wallspike rWallspikes;

    // Start is called before the first frame update
    void Start()
    {
        //Set Game object inactive so game doesn't start till button pressed
        character.SetActive(false);
        scoreUI.SetActive(false);
        lWallspikes = GameObject.Find("Left Wallspikes").GetComponent<Wallspike>();
        rWallspikes = GameObject.Find("Left Wallspikes").GetComponent<Wallspike>();
    }
    public void StartButtonPressed()
    {
        //Start game after button press
        mainMenu.SetActive(false);
        StartCoroutine(StartGame());
        lWallspikes.Shuffle();
        rWallspikes.Shuffle();
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
        gameOver.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        //Returns to Main Menu Button
        gameOver.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PlayAgain()
    {
        //Play Again button
        gameOver.SetActive(false);
        StartCoroutine(StartGame());
        lWallspikes.Shuffle();
        rWallspikes.Shuffle();
    }
}
