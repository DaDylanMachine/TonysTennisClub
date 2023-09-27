using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //I'm gonna be cash-money with y'all, I have no idea what the fuck this means.
    //I know it allows us to reference the player/target in the EnemyController script.
    //I'll figure out what this is later.
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    //GameObject and Component variables to edit them within the script.
    private GameObject item;
    public GameObject equippedItem;
    public GameObject player;
    public GameObject pickupText;
    public GameObject fullInventoryText;
    public GameObject itemPosition;
    public Camera playerCamera;
    //LayerMask variable to denote what layer the Raycast is looking for.
    public LayerMask pickupMask;
    //Float variable that determines the distance of the Raycast.
    public float pickupRange = 10f;
    //Boolean variable that keeps track if an item is equipped or not.
    private bool itemEquipped;
    //Boolean vatiable that keeps track if the inventory is full or not.
    private bool fullInventory;

    // Start is called before the first frame update
    void Start()
    {
        //Make sure the text is off.
        pickupText.SetActive(false);
        fullInventoryText.SetActive(false);
    }

    private void Update()
    {
        //If there are under 3 items on the player, the inventory isnt full. Otherwise, it is full.
        if (itemPosition.transform.childCount < 3)
            fullInventory = false;
        else
            fullInventory = true;

        //Runs the Drop function if "Q" is pressed and an item is equipped.
        if (Input.GetKeyDown(KeyCode.Q) && itemEquipped == true)
        {
            Drop();
        }

        //Send a Raycast and check if that Raycast hits an item if "E" is pressed.
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Raycast is made.
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            //Check if the raycast hits an item.
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
            {
                //If the players inventory is full, run the corutine that indicates to the player that info. Else, equip the item.
                if (fullInventory == true)
                {
                    StartCoroutine(DisplayFullInventory());
                }
                else
                {
                    item = hitInfo.collider.gameObject;
                    Equip();
                }
            }
  
        }
    }

    private void FixedUpdate()
    {
        //I've been using this just for debug purposes.
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log(fullInventory);
        }
    }

    //Function for equipping an item.
    void Equip()
    {
        //If the item is not the first item picked up, deactivate it.
        if (itemPosition.GetComponent<Transform>().childCount >= 1)
        {
            item.SetActive(false);
            pickupText.SetActive(false);
        }
        //Places the item in the EquipPosition.
        item.transform.SetPositionAndRotation(itemPosition.transform.position, itemPosition.transform.rotation);

        //Sets the item to the equipped state.
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<BoxCollider>().enabled = false;

        //Places the item as a child of EquipPosition.
        item.transform.SetParent(itemPosition.GetComponent<Transform>());

        //Denotes that an item is equipped.
        itemEquipped = true;

    }

    //Function for dropping an item.
    void Drop()
    {
        //Determines which item is currently equipped and puts it into a variable.
        equippedItem = itemPosition.transform.GetChild(itemPosition.GetComponent<ItemSwap>().selectedItem).gameObject;

        //Sets the equipped item back to a pickupable state.
        equippedItem.GetComponent<Rigidbody>().isKinematic = false;
        equippedItem.GetComponent<BoxCollider>().enabled = true;

        //Removes the equipped item under EquipPosition GameObject.
        equippedItem.transform.SetParent(null);

        if (itemPosition.GetComponent<Transform>().childCount == 0)
            itemEquipped = false;
        else
            itemPosition.GetComponent<ItemSwap>().SelectItem();
    }

    //Coroutine that displays text that indicates the inventory is full and the after some time, hides the text.
    IEnumerator DisplayFullInventory()
    {
        fullInventoryText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        fullInventoryText.SetActive(false);
    }
}
