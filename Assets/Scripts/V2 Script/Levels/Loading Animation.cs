// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using System.Collections;

// public class LoadingAnimation : MonoBehaviour
// {
//     [Header("Assign the LoadingIndicator UI Image")]
//     public Image loadingIndicator;

//     // Duration for the loading animation in seconds.
//     public float animationDuration = 2f;

//     // The scene name to load (set dynamically via the button).
//     private string sceneToLoad;

//     // This method starts the loading animation, taking the scene name as a parameter.
//     public void StartLoading(string sceneName)
//     {
//         sceneToLoad = sceneName;
//         // Enable the LoadingIndicator so it becomes visible.
//         loadingIndicator.gameObject.SetActive(true);
//         // Reset the fill to 0.
//         loadingIndicator.fillAmount = 0f;
//         // Start the animation coroutine.
//         StartCoroutine(AnimateLoading());
//     }

//     private IEnumerator AnimateLoading()
//     {
//         float elapsed = 0f;
//         while (elapsed < animationDuration)
//         {
//             elapsed += Time.deltaTime;
//             loadingIndicator.fillAmount = Mathf.Clamp01(elapsed / animationDuration);
//             yield return null;
//         }

//         // Ensure the indicator is fully filled.
//         loadingIndicator.fillAmount = 1f;

//         // Once complete, load the scene.
//         SceneManager.LoadScene(sceneToLoad);
//     }
// }


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingAnimation : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject loadingPanel; // The panel containing the loading UI
    public Slider loadingSlider; // The slider for the loading progress
    public float minLoadingTime = 3f; // Minimum time before allowing scene activation

    [Header("Audio Elements")]
    public AudioSource audioSource; // Single AudioSource
    public AudioClip normalPanelClip; // Sound when panel is open
    public AudioClip loadingClip; // Sound during loading

    private string sceneToLoad;

    private void Awake()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(false);

        // Play the normal panel sound at the beginning
        if (audioSource != null && normalPanelClip != null)
        {
            audioSource.clip = normalPanelClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StartLoading(string sceneName)
    {
        Debug.Log("Starting to load scene: " + sceneName);
        Time.timeScale = 1;
        sceneToLoad = sceneName;
        loadingSlider.value = 0f;
        loadingPanel.SetActive(true);

        // Switch to loading sound
        if (audioSource != null && loadingClip != null)
        {
            audioSource.clip = loadingClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float elapsedTime = 0f;

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress (0 to 1)

            // Update the slider using both progress and time
            loadingSlider.value = Mathf.Clamp01(elapsedTime / minLoadingTime * progress);

            if (operation.progress >= 0.9f && elapsedTime >= minLoadingTime)
            {
                loadingSlider.value = 1f;
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        // Wait a moment after scene activation
        yield return new WaitForSeconds(1f);

        // Disable the loading panel
        loadingPanel.SetActive(false);

        // Switch back to normal panel sound
        if (audioSource != null && normalPanelClip != null)
        {
            audioSource.clip = normalPanelClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
