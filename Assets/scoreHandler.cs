using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    private const string HIGH_SCORE_KEY = "HighScore";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);
        }
    }

    public void UpdateScore(int newScore)
    {
        int currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        if (newScore > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, newScore);
            PlayerPrefs.Save();
        }

        if (currentScoreText != null)
        {
            currentScoreText.text = $"Score: {newScore}";
        }
        
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {PlayerPrefs.GetInt(HIGH_SCORE_KEY)}";
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY);
    }
}