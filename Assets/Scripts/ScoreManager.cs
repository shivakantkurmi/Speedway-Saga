using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;  // Store the player's current score
    public TextMeshProUGUI scoreText;  // UI element to display score


    private void Start()
    {
        // Ensure the score is reset when the game starts
        ResetScore();
        // Debug.Log("ScoreManager Start: " + score);
    }

    // Method to add points to the score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    // Method to update the score UI
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Method to reset the score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
        // Debug.Log("Score reset to: " + score);  // Debug log to ensure it's resetting
    }
}
