using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
    public GameObject ReadyUI; // New UI element for the preparation screen
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    [SerializeField]
    private int score = 0;

    public GameState currentState;

    public PostProcessingSwitcher postProcessingSwitcher;

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

        if (GameState.Playing == currentState)
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
            StartCoroutine(StartPlayingWithDelay());
        }
        else if (state == GameState.GameOver)
        {
            Debug.Log("Game Over");
            // Show Game Over screen
        }
    }

    void OnStateExit(GameState state)
    {
        Debug.Log($"Exiting {state}");
        score = 0;
    }

    IEnumerator StartPlayingWithDelay()
    {
        postProcessingSwitcher.SwitchToGameplayProfile(); // Switch to gameplay profile
        ReadyUI.SetActive(true); // Show "Get Ready" UI
        InGameUI.SetActive(false);
        GameOverUI.SetActive(false);

        playerMovement.enabled = false; // Disable player movement during preparation

        yield return new WaitForSeconds(3); // Wait for 3 seconds

        ReadyUI.SetActive(false); // Hide "Get Ready" UI
        InGameUI.SetActive(true);

        playerMovement.enabled = true; // Enable player movement

        Debug.Log("Game Started");
    }

    void HandleMainMenu()
    {
        SceneManager.LoadScene(1); // Load the Main Menu scene
    }

    void HandlePlaying()
    {
        livesText.text = "Lives: " + playerHealth.health.ToString();
        scoreText.text = score.ToString();

        heart1.SetActive(playerHealth.health >= 1);
        heart2.SetActive(playerHealth.health >= 2);
        heart3.SetActive(playerHealth.health >= 3);

        if (playerHealth.GameOver)
        {
            ChangeState(GameState.GameOver);
        }
    }

    void HandleGameOver()
    {
        GameOverUI.SetActive(true);
        InGameUI.SetActive(false);
        playerMovement.playerSpeed = 0;
    }

    public void restartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void goToMainMenu()
    {
        postProcessingSwitcher.SwitchToMainMenuProfile(); // Switch to main menu profile
        ChangeState(GameState.MainMenu);
    }
}
