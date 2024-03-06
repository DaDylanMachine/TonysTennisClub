using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCooler : MonoBehaviour
{
    public Camera playerCamera;
    public PlayerManager rayRange;
    public GameObject checkList;
    [SerializeField] private GameObject fillText;

    private void Awake()
    {
        fillText = GameObject.FindGameObjectWithTag("CoolerText");
        // Sets the text to inactive since the above find method cannot find GameObjects that are inactive.
        fillText.SetActive(false);
    }

    // Function that displays that water can be filled when they hover over the water cooler.
    // Only works if the checklist item is put away.
    public void OnMouseOver()
    {
        if (!checkList.activeSelf)
        {
            // When the mouse is over the item, send a raycast.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            // If the item is within range, it activates the pickup text.
            if (Physics.Raycast(cameraRay, rayRange.pickupRange))
            {
                fillText.SetActive(true);
            }
        }
    }

    //Function that disables the deposit ttrash text when the mouse is not hovering over the can.
    public void OnMouseExit()
    {
        fillText.SetActive(false);
    }
}