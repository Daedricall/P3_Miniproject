using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    public GameObject finishText;           // Reference to the TextMeshPro prefab

    public Camera myCam;            // Reference to your custom camera

    private void Start()
    {
        ShowFinishPopup();
    }

    void ShowFinishPopup()
    {
        // Instantiate the TextMeshPro popup
        GameObject popup = Instantiate(finishText, transform.position, Quaternion.identity);

        // Make the popup a child of the camera
        if (myCam != null)
        {
            popup.transform.SetParent(myCam.transform);

            // Position the popup slightly in front of the camera
            popup.transform.localPosition = new Vector3(1, -1.25f, 1); // Adjust Z as needed
            popup.transform.localRotation = Quaternion.identity; // Reset rotation to face forward
        }
    }
}
