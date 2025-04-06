using UnityEngine;

public class PersistentSingleton : MonoBehaviour
{
    private static PersistentSingleton instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }
}
