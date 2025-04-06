using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public ScoreManager scoreManager; // Reference to the ScoreManager
    public GameObject gameOverUI;     // Reference to the Game Over UI

    private bool gameStarted; // Flag to check if the game has started
    private Rigidbody playerRigidbody; // Reference to the player's Rigidbody
    private AudioSource audioSource; // Reference to the AudioSource

    [Header("Sound Effects")]
    public AudioClip diamondSound; // Sound for collecting diamonds
    public AudioClip gameOverSound; // Sound for hitting an obstacle

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component

        if (playerRigidbody != null)
        {
            // Freeze rotation to prevent player from rotating on collision
            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        gameStarted = true; // Set to true once player is in the scene
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameStarted) return;

        if (collision.gameObject.CompareTag("Diamond"))
        {
            // Increase score and destroy the diamond
            scoreManager.AddScore(1);
            
            // Play diamond collection sound
            if (diamondSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(diamondSound);
            }

            Destroy(collision.gameObject); // Destroy the diamond
            // Debug.Log("Diamond collected!");
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Handle game over when colliding with an obstacle
            // Debug.Log("Game Over!");
            
            // Play game over sound
            if (gameOverSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(gameOverSound);
            }

            Time.timeScale = 0; // Stop the game
            gameOverUI.SetActive(true); // Activate Game Over UI
        }
    }

    // Call this method for restarting the game
    public void RestartGame()
    {
        Time.timeScale = 1; // Reset the game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the scene
    }

    // Call this method for quitting the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
        Application.Quit(); // Quit the application
        // Debug.Log("Game is quitting...");
    }
}
