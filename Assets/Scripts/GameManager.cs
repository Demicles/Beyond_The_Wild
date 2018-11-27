using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Players in party stats
    public CharStats[] playerStats;

    // To determine if player can move
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;

    // To manage the player inventory
    public string[] itemInventory;
    public int[] numberOfItems;
    public Item[] refItems;

    public int currentGold;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        SortItems();
    }
	
	// Update is called once per frame
	void Update () {
        // To determine if player can move
        if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }

    }

    public Item GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < refItems.Length; i++)
        {
            if(refItems[i].itemName == itemToGrab)
            {
                return refItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemInventory.Length - 1; i++)
            {
                if (itemInventory[i] == "")
                {
                    itemInventory[i] = itemInventory[i + 1];
                    itemInventory[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemInventory[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int itemPosition = 0;
        bool foundSlot = false;

        for(int i = 0; i < itemInventory.Length; i++)
        {
            if(itemInventory[i] == "" || itemInventory[i] == itemToAdd)
            {
                itemPosition = i;
                i = itemInventory.Length;
                foundSlot = true;
            }
        }

        if(foundSlot)
        {
            bool itemExist = false;

            for(int i = 0; i < refItems.Length; i++)
            {
                if(refItems[i].itemName == itemToAdd)
                {
                    itemExist = true;
                    i = refItems.Length;
                    
                }
            }

            if(itemExist)
            {
                itemInventory[itemPosition] = itemToAdd;
                numberOfItems[itemPosition]++;
            } else
            {
                Debug.LogError(itemToAdd + " does not exist!");
            }
        }

        GameMenu.instance.ShowItems();

    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemInventory.Length;
            }
        }

        if(foundItem)
        {
            numberOfItems[itemPosition]--;

            if(numberOfItems[itemPosition] <= 0)
            {
                itemInventory[itemPosition] = "";
                GameMenu.instance.itemName.text = "";
                GameMenu.instance.itemDescription.text = "";
            }

            GameMenu.instance.ShowItems();

        } else
        {
            Debug.LogError(itemToRemove + " couldn't be found!");
        }

    }

    public bool CanSell(string itemName)
    {
        bool canSell = false;

        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] == itemName)
            {
                if(numberOfItems[i] > 0)
                {
                    canSell = true;
                }
            }
        }

        return canSell;
    }

}
