using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {

    public Image buttonImage;
    public Text amountText;
    public int buttonID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectItemUpdate()
    {
        if(GameManager.instance.itemInventory[buttonID] != "")
        {
            GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemInventory[buttonID]));
        }
    }
}
