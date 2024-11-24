using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public LayerMask whatIsFinishLine;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private bool canRotate = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!canRotate) return; // Exit early if rotation is disabled

        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Move camera with mouse
        yRotation += mouseX;
        xRotation -= mouseY;

        // Makes sure you can only look up or down 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void DisableRotation()
    {
        canRotate = false; // Assuming you already added the canRotate flag earlier
        Cursor.lockState = CursorLockMode.None; // Optional, ensure the cursor is visible
        Cursor.visible = true;
    }
}
