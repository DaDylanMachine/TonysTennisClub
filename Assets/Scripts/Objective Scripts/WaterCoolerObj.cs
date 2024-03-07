using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WaterCoolerObj : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    public GameObject waterCooler1;
    public GameObject waterCooler2;
    public GameObject waterCooler3;
    public GameObject waterCooler4;
    public Camera playerCamera;
    public PlayerManager playerManager;
    public ItemSwap itemSwap;
    public Transform equipChild;
    // Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;
    // Int variable that keeps track of how much the water is filled
    public int waterFilled = 0;

    // Update is called once per frame
    private void Update()
    {
        if (waterCooler1.GetComponent<WaterCooler>().coolerFill == 25)
        {
            waterFilled += 1;
            waterCooler1.GetComponent<WaterCooler>().coolerFill = -1;
        }
        if (waterCooler2.GetComponent<WaterCooler>().coolerFill == 25)
        {
            waterFilled += 1;
            waterCooler2.GetComponent<WaterCooler>().coolerFill = -1;
        }
        if (waterCooler3.GetComponent<WaterCooler>().coolerFill == 25)
        {
            waterFilled += 1;
            waterCooler3.GetComponent<WaterCooler>().coolerFill = -1;
        }
        if (waterCooler4.GetComponent<WaterCooler>().coolerFill == 25)
        {
            waterFilled += 1;
            waterCooler4.GetComponent<WaterCooler>().coolerFill = -1;
        }

    }
}
