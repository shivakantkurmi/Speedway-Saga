using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Set the scene name in the Inspector
    public Button changeSceneButton; // Assign your UI button in the Inspector

    void Start()
    {
        if (changeSceneButton != null)
        {
            changeSceneButton.onClick.AddListener(ChangeScene);
        }
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty! Assign a scene name in the Inspector.");
        }
    }
}