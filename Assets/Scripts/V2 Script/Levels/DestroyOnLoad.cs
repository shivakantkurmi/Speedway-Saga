using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene change
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Destroy(gameObject); // Destroy this object on scene change
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }
}
