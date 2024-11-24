using UnityEngine;
using TMPro;

public class TimerPopup : MonoBehaviour
{
    public GameObject timerPrefab;   // Reference to the TextMeshPro prefab
    public Camera myCam;             // Reference to your custom camera

    private TextMeshPro timerText;   // Reference to the TextMeshPro component
    private float elapsedTime = 0f; // Timer value
    private bool isTiming = true;   // Timer state

    private void Start()
    {
        // Create and set up the timer
        SetupTimerPopup();
    }

    private void Update()
    {
        if (isTiming)
        {
            elapsedTime += Time.deltaTime; // Increment the timer
            UpdateTimerDisplay();         // Update the display
        }
    }

    void SetupTimerPopup()
    {
        // Instantiate the TextMeshPro timer
        GameObject popup = Instantiate(timerPrefab, transform.position, Quaternion.identity);

        // Attach the timer to the camera
        if (myCam != null)
        {
            popup.transform.SetParent(myCam.transform);

            // Position the timer in front of the camera
            popup.transform.localPosition = new Vector3(1, -0.78f, 1); // Adjust as needed
            popup.transform.localRotation = Quaternion.identity; // Reset rotation to face forward
        }

        // Get the TextMeshPro component
        timerText = popup.GetComponent<TextMeshPro>();
    }

    void UpdateTimerDisplay()
    {
        // Format the elapsed time as "Seconds:Milliseconds"
        timerText.text = elapsedTime.ToString("F2");
    }

    public void StopTimer()
    {
        isTiming = false; // Stop the timer
    }
}