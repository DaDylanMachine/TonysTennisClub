using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCoolerObj : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    public GameObject waterCooler;
    public Camera playerCamera;
    public PlayerManager playerManager;
    public ItemSwap itemSwap;
    public Transform equipChild;
    // Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;
    // Int variable that keeps track of how many pieces of trash have been thrown away.
    public int waterFilled = 0;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            // Raycast is made.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            // Check if the raycast hits a colldier.
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange))
            {
                // Checks if the raycast is hitting the water cooler GameObject and if the item they are holding is a bucket.
                if (hitInfo.collider.tag == "WaterCooler" && playerManager.equippedItem)
                {
                    if (playerManager.equippedItem.CompareTag("Bucket"))
                    {
                        Debug.Log("Water Cooler Being filled");
                    }
                }
            }
        }
    }
}
