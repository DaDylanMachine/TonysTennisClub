using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    // Variables for the sensitivity of  the mouse, the player, and the rotation.
    public float mouseSensitivity = 100f;
    public Transform player;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Sets it so the cursor doesn't move, stays in the middle of the screen, and it's invisible.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Gets the mouse input of up & down and left & right.
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Sets up clamping, enforcing that the camera can only look 180 degerees up & down.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // Rotates the whole player around the Y axis (siginified by the Vector3.up part) based off of the left & right input of the mouse.
        player.Rotate(Vector3.up * mouseX);
        // Same thing as the previous line but for up & down and is in a different format because this works with clamping.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
