using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public SegmentCreator segmentCreator;

    private bool starterSegmentscreated = false;

    private float elapsedTime = 0;

    [Header("UI Elements")]
    public GameObject InGameUI;
    public GameObject GameOverUI;
    public GameObject MainMenuUI;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;



    [SerializeField]
    private int score = 0;





    public GameState currentState;

    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }

    void Start()
    {
        ChangeState(GameState.Playing);
    }

    void Update()
    {


        switch (currentState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }


        if(GameState.Playing == currentState)
        {
                    // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate score based on elapsed time (e.g., 10 points per second)
        score = Mathf.FloorToInt(elapsedTime * 10);
        }
    }

    public void ChangeState(GameState newState)
    {
        // Optional: Clean up the old state
        OnStateExit(currentState);

        // Change to the new state
        currentState = newState;

        // Optional: Initialize the new state
        OnStateEnter(newState);
    }

    void OnStateEnter(GameState state)
    {
        if (state == GameState.MainMenu)
        {
            Debug.Log("Entering Main Menu");
            // Initialize Main Menu
        }
        else if (state == GameState.Playing)
        {
            Debug.Log("Starting Game");
            // Start gameplay
        }
        else if (state == GameState.GameOver)
        {
            Debug.Log("Game Over");
            // Show Game Over screen
        }
    }

    void OnStateExit(GameState state)
    {
        // Clean up resources if needed
        Debug.Log($"Exiting {state}");

        score = 0;
    }

    void HandleMainMenu()
    {
        GameOverUI.SetActive(false);
        InGameUI.SetActive(false);
        MainMenuUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space)) // Example input to start
        {
            ChangeState(GameState.Playing);
        }
    }

    void HandlePlaying()
    {

        if(starterSegmentscreated == false)
        {
            segmentCreator.PlaceStartSegments();
            starterSegmentscreated = true;
        }

        



        GameOverUI.SetActive(false);
        InGameUI.SetActive(true);
        MainMenuUI.SetActive(false);

        livesText.text = "Lives: " + playerHealth.health.ToString();
        scoreText.text = "Score: " + score.ToString();



        if (playerHealth.GameOver == true) // Example input to end game
        {
            ChangeState(GameState.GameOver);
        }
    }

    void HandleGameOver()
    {
        // Logic for Game Over

        GameOverUI.SetActive(true);
        InGameUI.SetActive(false);
        MainMenuUI.SetActive(false);

        Debug.Log("Gayme Over");


        playerMovement.playerSpeed = 0;

        /*
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        if (Input.GetKeyDown(KeyCode.R)) // Restart game
        {
            ChangeState(GameState.MainMenu);
        }
        */
    }

    public void restartGame () {

        playerHealth.health = 3;
        score = 0;
        elapsedTime = 0;
        playerMovement.ResetPlayer();
        segmentCreator.DestroyStartSegments();
        starterSegmentscreated = false;
        

        ChangeState(GameState.Playing);
    }


}

