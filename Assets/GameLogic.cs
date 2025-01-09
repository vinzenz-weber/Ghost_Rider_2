using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
    public PlayerHealth playerHealth;





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
    }

    void HandleMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Example input to start
        {
            ChangeState(GameState.Playing);
        }
    }

    void HandlePlaying()
    {
        if (playerHealth.GameOver == true) // Example input to end game
        {
            ChangeState(GameState.GameOver);
        }
    }

    void HandleGameOver()
    {
        // Logic for Game Over

        Debug.Log("Gayme Over");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        if (Input.GetKeyDown(KeyCode.R)) // Restart game
        {
            ChangeState(GameState.MainMenu);
        }
    }


}

