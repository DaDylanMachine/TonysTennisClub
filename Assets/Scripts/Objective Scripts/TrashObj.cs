using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObj : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    public GameObject trashCan;
    public Camera playerCamera;
    public PlayerManager playerManager;
    public ItemSwap itemSwap;
    public Transform equipChild;
    // Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;
    // Int variable that keeps track of how many pieces of trash have been thrown away.
    public int trashDeleted = 0;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast is made.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            // Check if the raycast hits a colldier.
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange))
            {
                // Checks if the raycast is hitting the trash can GameObject and if the item they are holding is trash.
                if (hitInfo.collider.tag == "TrashCan" && playerManager.equippedItem)
                {
                    if (playerManager.equippedItem.CompareTag("Trash"))
                    {
                        // If it is, destroy the trash item, denote that there is no equipped item, and track that another piece of trash has been deleted.
                        gameObject.GetComponent<AudioSource>().Play();
                        playerManager.GetRidOfItem();
                        trashDeleted++;
                    }
                }               
            }
        }
    }
}
