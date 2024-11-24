using UnityEngine;
using TMPro;
using UnityEngine.UI; // Required for Button component
using System.Collections.Generic; // Required for List

#if UNITY_EDITOR
using UnityEditor; // Required for EditorApplication.isPlaying when QuitGame is called, so that it also quits the editor
#endif

public static class GameData
{
    public static List<float> completionTimes = new List<float>(); // Store completion times, saved across scene reloads if doing "GameData.completionTimes.Count"
}

public class Popups : MonoBehaviour
{
    public GameObject startText;          // Reference to the TextMeshPro prefab for the start popup
    public GameObject finishTextPrefab;  // Reference to the TextMeshPro prefab for the finish popup
    public Camera myCam;                 // Reference to your custom camera
    public float duration = 10f;         // Duration of the fade-out

    private bool finished;
    private float playerHeight = 2;
    public LayerMask whatIsFinishLine;

    private float elapsedTime; // Current elapsed time
    private bool isTiming = true; // Timer state

    private void Start()
    {
        ShowStartPopup();
    }

    private void Update()
    {
        if (isTiming)
        {
            elapsedTime += Time.deltaTime; // Increment timer
        }
    }


    private void FixedUpdate()
    {
        finished = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.2f, whatIsFinishLine);

        if (finished && isTiming)
        {
            Finish();
        }
    }

    void ShowStartPopup()
    {
        // Instantiate the TextMeshPro popup
        GameObject popup = Instantiate(startText, transform.position, Quaternion.identity);

        // Make the popup a child of the custom camera
        if (myCam != null)
        {
            popup.transform.SetParent(myCam.transform);

            // Position the popup slightly in front of the camera
            popup.transform.localPosition = new Vector3(1, -0.78f, 1); // Adjust Z as needed
            popup.transform.localRotation = Quaternion.identity; // Reset rotation to face forward
        }

        // Start fading out the text over 5 seconds
        StartCoroutine(FadeOutText(popup.GetComponent<TextMeshPro>()));
    }

    System.Collections.IEnumerator FadeOutText(TextMeshPro textMeshPro)
    {

        float elapsedTime = 0f;
        Color originalColor = textMeshPro.color;

        while (elapsedTime < duration)
        {
            // Gradually reduce the alpha value of the text color
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Destroy the popup after fading out
        Destroy(textMeshPro.gameObject);
    }

    public void Finish()
    {
        isTiming = false; // Stop the timer
        GameData.completionTimes.Add(elapsedTime); // Save the completion time
        GameData.completionTimes.Sort(); // Sort times (shortest first)

        // Notify TimerPopup to hide the timer
        TimerPopup timerPopup = FindFirstObjectByType<TimerPopup>();
        if (timerPopup != null)
        {
            timerPopup.HideTimer();
        }

        // Disable player movement
        PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.DisableMovement();
        }

        // Disable camera rotation
        PlayerCam playerCam = FindFirstObjectByType<PlayerCam>();
        if (playerCam != null)
        {
            playerCam.DisableRotation(); // Call the new method in PlayerCam
        }

        GameObject popup = Instantiate(finishTextPrefab, transform.position, Quaternion.identity);
                // Make sure the canvas is correctly referenced
        Canvas canvas = FindFirstObjectByType<Canvas>(); // Or directly reference the canvas if you have it
                                                         // Parent the instantiated object to the canvas
        if (canvas != null)
        {
            popup.transform.SetParent(canvas.transform, false); // false keeps local position intact
            popup.transform.localPosition = new Vector3(0, -10f, 0);
            popup.transform.localRotation = Quaternion.identity;
        }

        // Update the finish text with the current time and top 5 completions
        TextMeshPro textMeshPro = popup.GetComponent<TextMeshPro>();
        if (textMeshPro != null)
        {
            string leaderboard = "Completion time: " + elapsedTime.ToString("F2") + " seconds\n\n";
            leaderboard += "Top 3 Highscores:\n";
            for (int i = 0; i < Mathf.Min(3, GameData.completionTimes.Count); i++)
            {
                leaderboard += $"{i + 1}. {GameData.completionTimes[i]:F2} seconds\n";
            }
            textMeshPro.text = leaderboard;
        }

        // Get the specific buttons by name or other identifiers
        Button restartButton = popup.transform.Find("RestartButton").GetComponent<Button>();
        Button quitButton = popup.transform.Find("QuitButton").GetComponent<Button>();

        // Assign listeners to each button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() => RestartGame());
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(() => QuitGame());
        }
    }

    public void RestartGame()
    {
        // Logic to restart the game (e.g., reload the scene)
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Exit play mode in the Unity Editor
        EditorApplication.isPlaying = false;
        #else
        // Quit the application in a build
        Application.Quit();
        #endif

        Debug.Log("QuitGame called.");
    }
}