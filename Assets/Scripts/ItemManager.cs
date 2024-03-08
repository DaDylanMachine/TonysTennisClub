using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject checkList;
    public GameObject pickupText;
    public Camera playerCamera;
    public PlayerManager rayRange;

    //Item Index to be used for the inventory
    public int inventoryIndex = -1;

    // Functions that display text informing the player they can pickup an item when they hover over it.
    // Only occurs if the checklist item is put away.
    public void OnMouseOver()
    {
        if (!checkList.activeSelf)
        {
            // When the mouse is over the item, send a raycast.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            // If the item is within range, it activates the pickup text.
            if (Physics.Raycast(cameraRay, rayRange.pickupRange))
            {
                pickupText.SetActive(true);
            }
        }
    }

    public void OnMouseExit()
    {
        pickupText.SetActive(false);
    }
}
