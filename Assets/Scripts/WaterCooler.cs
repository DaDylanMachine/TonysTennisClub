using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCooler : MonoBehaviour
{
    public Camera playerCamera;
    public PlayerManager playerManager;
    public PlayerManager rayRange;
    public GameObject checkList;
    public GameObject waterCooler;
    //[SerializeField] private GameObject fillText;
    public GameObject fillText;
    // Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;

    //Individual Cooler Fill Level
    public float coolerFill = 0f;

    private void Awake()
    {

        //fillText = GameObject.FindGameObjectWithTag("CoolerText");
        // Sets the text to inactive since the above find method cannot find GameObjects that are inactive.
        if (fillText.activeSelf)
        fillText.SetActive(false);
    }

    private void Update()
    {
        //Set Water Fill to 100 when overfilled
        if (coolerFill > 25)
            coolerFill = 25;

        if (Input.GetKey(KeyCode.E))
        {
            // Raycast is made.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            // Check if the raycast hits a colldier.
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange))
            {
                // Checks if the raycast is hitting the water cooler GameObject and if the item they are holding is a bucket.
                if (hitInfo.collider.tag == "WaterCooler" && playerManager.equippedItem && hitInfo.collider.gameObject == waterCooler)
                {   //Checks Bucket Tag and if the bucketFill on the bucket is greater than 0
                    if (playerManager.equippedItem.CompareTag("Bucket") && playerManager.equippedItem.GetComponent<WaterBucket>().bucketFill > 0 && coolerFill < 25 && coolerFill != -1)
                    {
                        //Increments the Water Cooler fill up while decrementing the bucket fill down
                        coolerFill += playerManager.equippedItem.GetComponent<WaterBucket>().bucketFillSpeed;
                        playerManager.equippedItem.GetComponent<WaterBucket>().bucketFill -= playerManager.equippedItem.GetComponent<WaterBucket>().bucketFillSpeed;
                    }
                }
            }
        }
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
            if (Physics.Raycast(cameraRay, rayRange.pickupRange) && rayRange.equippedItem)
            {
                if (rayRange.equippedItem.CompareTag("Bucket"))
                {
                    fillText.SetActive(true);
                }
            }
        }
    }
    public void OnMouseExit()
    {
        fillText.SetActive(false);
    }
}