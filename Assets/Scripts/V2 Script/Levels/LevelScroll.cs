using UnityEngine;
using System.Collections;

public class LevelScroll : MonoBehaviour
{
    [Header("Assign the ContentPanel RectTransform (child of MaskPanel)")]
    public RectTransform contentPanel;

    // Fixed screen width for Full HD
    private float screenWidth = 1920f;

    // Total levels (4)
    private int totalLevels = 4;

    // Current level index (starting at 0 for Level 1)
    private int currentIndex = 0;

    // Duration of the slide in seconds (adjust as needed)
    public float slideDuration = 0.5f;

    void Start()
    {
        // Ensure the ContentPanel starts at level 1 (left-most panel)
        contentPanel.anchoredPosition = new Vector2(-currentIndex * screenWidth, contentPanel.anchoredPosition.y);
    }


    // Call this method to slide left (to previous level)
    public void ScrollLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(SmoothSlide());
        }
    }

    // Call this method to slide right (to next level)
    public void ScrollRight()
    {
        if (currentIndex < totalLevels - 1)
        {
            currentIndex++;
            StartCoroutine(SmoothSlide());
        }
    }

    // Coroutine that smoothly slides the contentPanel to the target position.
    private IEnumerator SmoothSlide()
    {
        Vector2 startPos = contentPanel.anchoredPosition;
        // With pivot (0, 0.5), the target is simply -currentIndex * screenWidth on the X axis.
        Vector2 targetPos = new Vector2(-currentIndex * screenWidth, startPos.y);

        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            // Use Mathf.Clamp01 to ensure t is between 0 and 1.
            float t = Mathf.Clamp01(elapsed / slideDuration);
            contentPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
        contentPanel.anchoredPosition = targetPos;
    }
}
