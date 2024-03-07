using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    //public GameObject waterBucket;
    public Camera playerCamera;
    public PlayerManager playerManager;
    // Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;
    //Float for bucket item
    public float bucketFill = 0f;
    //Float value for how fast to fill
    public float bucketFillSpeed = 0f;


    // Update is called once per frame
    private void Update()
    {
        if (bucketFill < 0)
        {
            bucketFill = 0;
        }else if (bucketFill > 25)
        {
            bucketFill = 25;
        }

        if (Input.GetKey(KeyCode.E))
        {
            // Raycast is made.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            // Check if the raycast hits a colldier.
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange))
            {
                // Checks if the raycast is hitting the Water Faucet and the equipped item is a bucket
                if (hitInfo.collider.tag == "WaterFaucet" && playerManager.equippedItem)
                {
                    if (playerManager.equippedItem.CompareTag("Bucket"))
                    {
                        // If it is, increase bucketFill by fill amount
                        if (bucketFill < 25)
                            bucketFill += bucketFillSpeed;
                    }
                }
            }
        }
    }
}