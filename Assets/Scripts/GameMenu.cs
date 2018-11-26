using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public GameObject theMenu;

    // For root page of menu 
    [Header("Menu root")]
    public GameObject[] windows;
    public GameObject[] charStatsHolder;
    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;

    // For the status subpage of characters
    [Header("Status subpage")]
    public Image statusImage;
    public GameObject[] statusButton;
    public Text statusName, statusHP, statusMP, statusStr, statusDef, StatusWpnEqpd, StatusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp;

    // For the inventory subpage of the party
    [Header("Inventory subpage")]
    public ItemButton[] itemButtons;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;
    public string selectedItem;

    private CharStats[] playerStats;

    public static GameMenu instance;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "LVL: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                charStatsHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

        public void CloseMenu()
        {
            for (int i = 0; i < windows.Length; i++)
            {
                windows[i].SetActive(false);
            }

            theMenu.SetActive(false);
            GameManager.instance.gameMenuOpen = false;
        }

    public void OpenStatusMenu()
    {
        UpdateMainStats();

        // Update the information that is shown
        StatusChar(0);

        for (int i = 0; i < statusButton.Length; i++)
        {
            statusButton[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButton[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int charSelect)
    {
        statusName.text = playerStats[charSelect].charName;
        statusHP.text = "" + playerStats[charSelect].currentHP + "/" + playerStats[charSelect].maxHP;
        statusMP.text = "" + playerStats[charSelect].currentMP + "/" + playerStats[charSelect].maxMP;
        statusStr.text = playerStats[charSelect].strenght.ToString();
        statusDef.text = playerStats[charSelect].defence.ToString();
        if (playerStats[charSelect].equippedWeapon != "")
        {
            StatusWpnEqpd.text = playerStats[charSelect].equippedWeapon;
        }
        StatusWpnPwr.text = playerStats[charSelect].weaponPower.ToString();
        if (playerStats[charSelect].equippedArmor != "")
        {
            statusArmrEqpd.text = playerStats[charSelect].equippedArmor;
        }
        statusArmrPwr.text = playerStats[charSelect].armorPower.ToString();
        statusExp.text = (playerStats[charSelect].expToNextLevel[playerStats[charSelect].playerLevel] - playerStats[charSelect].currentEXP).ToString();
        statusImage.sprite = playerStats[charSelect].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonID = i;

            if(GameManager.instance.itemInventory[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemInventory[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        if(activeItem.isUsable)
        {
            useButtonText.text = "Use";
        }

        if(activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;

    }

}
