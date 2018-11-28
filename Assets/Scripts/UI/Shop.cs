using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale = new string[40];

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;

    public static Shop instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "G";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);

        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyItemButtons[0].SelectItemUpdate();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonID = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }

    }

    public void OpenSellMenu()
    {
        sellItemButtons[0].SelectItemUpdate();

        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        UpdateSellMenu();
    }

    private void UpdateSellMenu()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonID = i;

            if (GameManager.instance.itemInventory[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemInventory[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = "Value: " + selectedItem.value.ToString() + "G";
    }

    public void SelectSellItem(Item sellItem)
    {
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.description;
        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "G";
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);

            }
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "G";
    }

    public void SellItem()
    {
        if(selectedItem != null && GameManager.instance.CanSell(selectedItem.itemName))
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);
            GameManager.instance.RemoveItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "G";

        UpdateSellMenu();
    }

}
