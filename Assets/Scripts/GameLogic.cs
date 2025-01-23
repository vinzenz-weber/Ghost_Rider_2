using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLogic : MonoBehaviour
{
   public PlayerHealth playerHealth;
   public PlayerMovement playerMovement;
   public SegmentCreator segmentCreator;
   public ScoreHandler scoreHandler;

   private bool starterSegmentscreated = false;
   private float elapsedTime = 0;
   private float gameOverTimer = 0f;
   private const float GAME_OVER_TIMEOUT = 20f;

   [Header("UI Elements")]
   public GameObject InGameUI;
   public GameObject GameOverUI; 
   public GameObject ReadyUI;
   public TextMeshProUGUI livesText;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI gameOverScoreText;
   public TextMeshProUGUI gameOverHighScoreText;

   public TextMeshProUGUI IngamaeHighscoreText;

   public GameObject heart1;
   public GameObject heart2;
   public GameObject heart3;

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

       if (GameState.Playing == currentState)
       {
           elapsedTime += Time.deltaTime;
           score = Mathf.FloorToInt(elapsedTime * 10);
           scoreHandler.UpdateScore(score);
       }
   }

   public void ChangeState(GameState newState)
   {
       OnStateExit(currentState);
       currentState = newState;
       OnStateEnter(newState);
   }

   void OnStateEnter(GameState state)
   {
       switch(state) 
       {
           case GameState.MainMenu:
               Debug.Log("Entering Main Menu");
               break;
           case GameState.Playing:
               Debug.Log("Starting Game");
               StartCoroutine(StartPlayingWithDelay());
               break;
           case GameState.GameOver:
               Debug.Log("Game Over");
               gameOverTimer = 0f;
               break;
       }
   }

   void OnStateExit(GameState state)
   {
       Debug.Log($"Exiting {state}");
       if (state == GameState.GameOver)
       {
           score = 0;
           elapsedTime = 0;
       }
   }

   IEnumerator StartPlayingWithDelay()
   {
       ReadyUI.SetActive(true);
       InGameUI.SetActive(false);
       GameOverUI.SetActive(false);
       playerMovement.enabled = false;

       yield return new WaitForSeconds(3);

       ReadyUI.SetActive(false);
       InGameUI.SetActive(true);
       playerMovement.enabled = true;
       Debug.Log("Game Started");
   }

   void HandleMainMenu()
   {
       SceneManager.LoadScene(1);
   }

   void HandlePlaying()
   {
       livesText.text = "Lives: " + playerHealth.health.ToString();
       scoreText.text = score.ToString();

       IngamaeHighscoreText.text = $"Highscore: {scoreHandler.GetHighScore()}";

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
       
       gameOverScoreText.text = $"Dein Score: {score}";
       gameOverHighScoreText.text = $"Highscore: {scoreHandler.GetHighScore()}";

       gameOverTimer += Time.deltaTime;
       if (gameOverTimer >= GAME_OVER_TIMEOUT)
       {
           gameOverTimer = 0f;
           goToMainMenu();
       }
   }

   public void restartGame()
   {
       string currentSceneName = SceneManager.GetActiveScene().name;
       SceneManager.LoadScene(currentSceneName);
   }

   public void goToMainMenu()
   {
       ChangeState(GameState.MainMenu);
   }
}