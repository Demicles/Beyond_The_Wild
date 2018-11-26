using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Players in party stats
    public CharStats[] playerStats;

    // To determine if player can move
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

    // To manage the player inventory
    public string[] itemInventory;
    public int[] numberOfItems;
    public Item[] refItems;

    // Use this for initialization
    void Start () {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        // To determine if player can move
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
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
}
