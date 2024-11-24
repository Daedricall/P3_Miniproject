using UnityEngine;
using TMPro;

public class ResetPosition : MonoBehaviour
{
    public float resetThresholdY = -10f;     // Y threshold for reset
    public Vector3 startPosition;            // Starting position of the player
    public GameObject youFellPrefab;         // Reference to the TextMeshPro prefab
    public Camera customCamera;              // Reference to your custom camera


    void Start()
    {
        // Save the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Check if the character's Y position is below the threshold
        if (transform.position.y < resetThresholdY)
        {
            ResetToStartPosition();
        }
    }

    void ResetToStartPosition()
    {
        
        
        transform.position = startPosition;

        // Stop Rigidbody movement if applicable
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Create and display the "You Fell!" popup
        ShowPopup();
    }

    void ShowPopup()
    {
        // Instantiate the TextMeshPro popup
        GameObject popup = Instantiate(youFellPrefab, transform.position, Quaternion.identity);

        // Make the popup a child of the custom camera
        if (customCamera != null)
        {
            popup.transform.SetParent(customCamera.transform);

            // Position the popup slightly in front of the camera
            popup.transform.localPosition = new Vector3(0.45f, -0.1f, 1.5f); // Adjust Z as needed
            popup.transform.localRotation = Quaternion.identity; // Reset rotation to face forward
        }

        // Start fading out the text over 5 seconds
        StartCoroutine(FadeOutText(popup.GetComponent<TextMeshPro>()));
    }

    System.Collections.IEnumerator FadeOutText(TextMeshPro textMeshPro)
    {
        float duration = 2f; // Duration of the fade-out
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
}