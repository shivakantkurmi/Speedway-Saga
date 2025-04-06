using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
        #else
            Application.Quit(); // Exit game in build
        #endif
    }
}
