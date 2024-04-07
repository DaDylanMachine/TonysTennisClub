using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    private GameObject item;
    public GameObject equippedItem;
    public GameObject player;
    public GameObject pickupText;
    public GameObject fullInventoryText;
    public GameObject itemPosition;
    public GameObject checkList;
    public GameObject uiChecklist;
    public TrashObj trashCan;
    public WaterCoolerObj waterCooler;
    public BrushCourtsObj brushCourts;
    public GameEnd endGame;
    public Toggle trashObjToggle;
    public Toggle waterObjToggle;
    public Toggle brushObjToggle;
    public Camera playerCamera;
    public Text trashObjText;
    public Text waterObjText;
    public Text brushObjText;
    // LayerMask variable to denote what layer the Raycast is looking for.
    public LayerMask pickupMask;
    public LayerMask cartMask;
    // Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;
    // Boolean variable that keeps track if an item is equipped or not.
    public bool itemEquipped;
    // Boolean vatiable that keeps track if the inventory is full or not.
    private bool fullInventory;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the text is off.
        pickupText.SetActive(false);
        fullInventoryText.SetActive(false);
        // References the variables to Components to use later in the script. Must be done here since the GameObject spawns after startup.
        trashObjToggle = GameObject.FindGameObjectWithTag("TrashToggle").GetComponent<Toggle>();
        trashObjText = GameObject.FindGameObjectWithTag("TrashText").GetComponent<Text>();
        waterObjToggle = GameObject.FindGameObjectWithTag("WaterToggle").GetComponent<Toggle>();
        waterObjText = GameObject.FindGameObjectWithTag("WaterText").GetComponent<Text>();
        brushObjToggle = GameObject.FindGameObjectWithTag("BrushToggle").GetComponent<Toggle>();
        brushObjText = GameObject.FindGameObjectWithTag("BrushText").GetComponent<Text>();
    }

 

    // Update is called once per frame
    private void Update()
    {
        if(trashObjToggle == null)
        {
            trashObjToggle = GameObject.FindGameObjectWithTag("TrashToggle").GetComponent<Toggle>();
        }
        if (trashObjText == null)
        {
            trashObjText = GameObject.FindGameObjectWithTag("TrashText").GetComponent<Text>();
        }
        if (trashObjToggle == null)
        {
            waterObjToggle = GameObject.FindGameObjectWithTag("WaterToggle").GetComponent<Toggle>();
        }
        if (waterObjToggle == null)
        {
            waterObjText = GameObject.FindGameObjectWithTag("WaterText").GetComponent<Text>();
        }
        if (waterObjText == null)
        {
            brushObjToggle = GameObject.FindGameObjectWithTag("BrushToggle").GetComponent<Toggle>();
        }
        if (brushObjToggle == null)
        {
            brushObjText = GameObject.FindGameObjectWithTag("BrushText").GetComponent<Text>();
        }

        if (trashObjToggle.isOn && waterObjToggle.isOn && brushObjToggle.isOn)
        {
            endGame.TransitionEnd();
        }

        if (isDead)
        {
            endGame.TransitionEnd();
        }


        // If there are under 3 items on the player, the inventory isnt full. Otherwise, it is full.
        if (itemPosition.transform.childCount < 3)
            fullInventory = false;
        else
            fullInventory = true;
        
        // Equips or unequips the checklist 
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (checkList.activeSelf)
            {
                checkList.SetActive(false);
                uiChecklist.SetActive(false);
                itemPosition.SetActive(true);
            }
            else
            {
                checkList.SetActive(true);
                uiChecklist.SetActive(true);
                itemPosition.SetActive(false);
                // Runs the check to see if any objectives have been completed while the checklist was put away.
                UpdateObjectives();
            }

        }
        
        // Ensures the following code only runs if the checklist is put away.
        if (!checkList.activeSelf)
        {
            // Runs the GetRidOfItem function if "Q" is pressed and an item is equipped.
            if (Input.GetKeyDown(KeyCode.Q) && itemEquipped == true)
            {
                dropItem();
            }

            // Send a Raycast and check if that Raycast hits an item if "E" is pressed.
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Raycast is made.
                Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
                // Check if the raycast hits an item.
                if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
                {
                    item = hitInfo.collider.gameObject;
                    // If the players inventory is full, run the corutine that indicates to the player that info. Else, equip the item.
                    if (fullInventory == true)
                    {
                        StartCoroutine(DisplayFullInventory());
                    }
                    else if (item.CompareTag("Cart"))
                    {
                        if(item.GetComponent<CarController>().player == null)
                        {
                            item.GetComponent<CarController>().enabled = true;
                            this.GetComponent<TonyRun>().enabled = false;
                            this.gameObject.transform.SetParent(item.transform);
                            item.GetComponent<CarController>().player = this.gameObject;
                            this.GetComponent<CharacterController>().enabled = false;
                            item.GetComponent<CarController>().player.transform.position = item.transform.GetChild(0).transform.position;
                        }
                    }
                    else
                    {
                        Equip();
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // I've been using this just for debug purposes.
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log(Time.deltaTime);
        }
    }

    // Function for equipping an item.
    void Equip()
    {
        //// If the item is not the first item picked up, deactivate it.
        //if (itemPosition.GetComponent<Transform>().childCount >= 1)
        //{
        //    item.SetActive(false);
        //    pickupText.SetActive(false);
        //}
        // Places the item in the EquipPosition.
        item.transform.SetPositionAndRotation(itemPosition.transform.position, item.transform.rotation);


        // Sets the item to the equipped state.
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().enabled = false;

        //Set Item Index for Inventory System to lowest available index in array
        for (int i = 0; i < 3; i++)
        {
            if (!itemPosition.GetComponent<ItemSwap>().inventoryArray[i].HasValue)
            {
                //if the current selected index does not equal the lowest empty slot selected, set item to inactive otherwise set item as active
                if (itemPosition.GetComponent<ItemSwap>().selectedItem != i)
                {
                    item.SetActive(false);
                    pickupText.SetActive(false);

                }
                else
                {
                    equippedItem = item;
                    itemEquipped = true;
                }
                //Set Inventory Index for item and in Inventory Array
                item.GetComponent<ItemManager>().inventoryIndex = i;
                itemPosition.GetComponent<ItemSwap>().inventoryArray[i] = i;
                break;
            }
                
        }

        // Places the item as a child of EquipPosition.
        item.transform.SetParent(itemPosition.GetComponent<Transform>());

        // Denotes that an item is equipped.

        // Determines which item is currently equipped and puts it into a variable.
        // 3/6/2024 Having bug issues where occasionally pick up will not be equipped
        // 3/7/2024 Removed and added equippedItem and item equipped as part of the index set loop
        // equippedItem = itemPosition.transform.GetChild(itemPosition.GetComponent<ItemSwap>().selectedItem).gameObject; 
    }

    // Function for dropping an item.
    public void GetRidOfItem()
    {

        // Sets the equipped item back to a pickupable state.
        //equippedItem.GetComponent<Rigidbody>().isKinematic = false;
        //equippedItem.GetComponent<Collider>().enabled = true;

        //Reset Index of Item and Inventory Array
        itemPosition.GetComponent<ItemSwap>().inventoryArray[equippedItem.GetComponent<ItemManager>().inventoryIndex] = null;
        equippedItem.GetComponent<ItemManager>().inventoryIndex = -1;

        // Removes the equipped item under EquipPosition GameObject.
        equippedItem.transform.SetParent(null);

        // Destroy the object if the item is trash.
        //if (equippedItem.CompareTag("Trash"))
        Destroy(equippedItem);

        // Check to see if the dropped item was the last item in the inventory, if not, swap to another item
        // 3/7/2024 Setting Item Equipped to false and equipped Item to Null whenever called
        //if (itemPosition.GetComponent<Transform>().childCount == 0)
        //{
        itemEquipped = false;
        equippedItem = null;
        //}           
        //else
        //    itemPosition.GetComponent<ItemSwap>().SelectItem();
    }
    public void dropItem() //3/5/2024 Created dropItem script just for the Q button
    {

        //Reset Index of Item and Inventory Array
        itemPosition.GetComponent<ItemSwap>().inventoryArray[equippedItem.GetComponent<ItemManager>().inventoryIndex] = null;
        equippedItem.GetComponent<ItemManager>().inventoryIndex = -1;

        // Sets the equipped item back to a pickupable state.
        equippedItem.GetComponent<Rigidbody>().isKinematic = false;
        equippedItem.GetComponent<Collider>().enabled = true;

        // Removes the equipped item under EquipPosition GameObject.
        equippedItem.transform.SetParent(null);

        // Check to see if the dropped item was the last item in the inventory, if not, swap to another item
        // 3/7/2024 Setting Item Equipped to false and equipped Item to Null whenever called
        //if (itemPosition.GetComponent<Transform>().childCount == 0)
        //{
            itemEquipped = false;
            equippedItem = null;
        //}
        //else
        //    itemPosition.GetComponent<ItemSwap>().SelectItem();
    }

    // Function to check if the objective for each task has been met.
    void UpdateObjectives()
    {
        // Update for the trash objective.
        // Checks how much trash has been thrown away and updates the label every time one is thrown away. If it reaches 5, mark the task as complete.
        switch (trashCan.trashDeleted)
        {
            case 5:
                trashObjToggle.isOn = true;
                goto default;
            default:
                trashObjText.text = "Throw Away 5 Pieces of Trash (" + trashCan.trashDeleted.ToString() + "/5)";
                break;
        }

        switch(waterCooler.waterFilled)
        {
            case 4:
                waterObjToggle.isOn = true;
                goto default;
            default:
                waterObjText.text = "Fill Up Water Coolers (" + waterCooler.waterFilled.ToString() + "/4)";
                break;
                

        }

        switch (brushCourts.brushedCourts)
        {
            case 2:
                brushObjToggle.isOn = true;
                goto default;
            default:
                brushObjText.text = "Brush the Courts with the Cart (" + brushCourts.brushedCourts.ToString() + "/2)";
                break;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Poggers");
        if (other.gameObject.tag == "Monster")
        {
            isDead = true;
        }
    }

    // Coroutine that displays text that indicates the inventory is full and the after some time, hides the text.
    IEnumerator DisplayFullInventory()
    {
        fullInventoryText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        fullInventoryText.SetActive(false);
    }
}
