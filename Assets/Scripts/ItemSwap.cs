using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwap : MonoBehaviour
{
    // Variable that helps denote which item in the inventory is equipped.
    public int selectedItem = 0;

    //Integer Array for Static Inventory System
    public int?[] inventoryArray = new int?[3];
    // Variable that will allow us to change the equippedItem variable in this script.
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        // Only run if the game spawns player in with multiple item.
        // SelectItem();
    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedItem = selectedItem;

        // Checks to see if player moves the scroll wheel.
        // "Mouse ScrollWheel" is a float that defines greater than 0 as a scroll up and less than 0 a scroll down.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // If the index goes above the amount children available, reset the index. Else, increase the index by one.
            if (selectedItem >= transform.childCount - 1)
                selectedItem = 0;
            else
                selectedItem++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // If the index goes below 0, set the index to one less thant the amount of children. Else, decrease the index by one.
            if (selectedItem <= 0)
            {
                selectedItem = transform.childCount - 1;
                if (selectedItem < 0)
                    selectedItem = 0; 
            }
            else
                selectedItem--;
        }

        // Checks to see if player presses one of the number keys 1-4 and if the player has that many items in their inventory.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) /*&& transform.childCount >= 2*/)
        {
            selectedItem = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) /*&& transform.childCount >= 3*/)
        {
            selectedItem = 2;
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        //{
        //    selectedItem = 3;
        //}

        // If the selected item has changed then run the SelectItem function.
        if (previousSelectedItem != selectedItem)
        {
            SelectItem();
        }
    }

    // Function for selecting which item is active.
    public void SelectItem()
    {
        // Iterator variable.
        //int i = 0;

        // Checks to see if the selectedItem variable is higher than the available indexes.
        //if (selectedItem > this.GetComponent<Transform>().childCount - 1)
        //{
        //    selectedItem--;
        //}

        //Check to see if selected index is valid in the array and set appropriate variable
            // Iterates through every item the player has in their inventory and checks which one should be equipped.
            foreach (Transform item in transform)
            {
                // If the iterator matches the value of the selected item, activate the item and if not, deactivate it.
                if (item.GetComponent<ItemManager>().inventoryIndex == selectedItem)
                {
                    item.gameObject.SetActive(true);
                    // Updates the value of the equippedItem so it's in line with what is actually equipped.
                    playerManager.equippedItem = item.gameObject;
                    playerManager.itemEquipped = true;
                }
                else
                {
                    item.gameObject.SetActive(false);
                }

            }
        //if the array value is empty for selected Item, set item equipped to false and equipped item to null
        if (!inventoryArray[selectedItem].HasValue)
        {
            playerManager.equippedItem = null;
            playerManager.itemEquipped = false;
        }

    }

}
