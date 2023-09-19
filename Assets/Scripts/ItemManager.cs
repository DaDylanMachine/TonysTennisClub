using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject pickupText;
    public Camera playerCamera;
    public GameObject playerManager;
    private PlayerManager rayRange;

    private void Start()
    {
        //Recieves the PlayerManager component (script) from the GameManager GameObject.
        //This is done to get the pickupRange variable from that script.
        rayRange = playerManager.GetComponent<PlayerManager>();
    }

    //Functions that display text informing the player they can pickup an item when they hover over it.
    public void OnMouseOver()
    {
        //When the mouse is over the item, send a raycast.
        Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        //If the item is within range, it activates the pickup text.
        if (Physics.Raycast(cameraRay, rayRange.pickupRange))
        {
            pickupText.SetActive(true);
        }
    }
    public void OnMouseExit()
    {
        pickupText.SetActive(false);
    }
}
