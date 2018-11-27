using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public Sprite itemSprite;

    [Header("Item Type")]
    public bool isUsable;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public int amountModifier;
    public bool affectHP, affectMP, affectSTR, affectDEF;

    [Header("Weapon/Armor details")]
    public int weaponStrength;
    public int armorStrength;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Use(int selectChar)
    {
        CharStats selectedChar = GameManager.instance.playerStats[selectChar];

        if(isUsable)
        {
            if(affectHP)
            {
                selectedChar.currentHP += amountModifier;

                if(selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }

            if (affectMP)
            {
                selectedChar.currentMP += amountModifier;

                if (selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }

            if(affectSTR)
            {
                selectedChar.strenght += amountModifier;
            }

            if (affectDEF)
            {
                selectedChar.defence += amountModifier;
            }

        }

        if(isWeapon)
        {
            if(selectedChar.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedWeapon);
            }

            selectedChar.equippedWeapon = itemName;
            selectedChar.weaponPower = weaponStrength;
        }

        if(isArmor)
        {
            if (selectedChar.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedArmor);
            }

            selectedChar.equippedArmor = itemName;
            selectedChar.armorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);

    }
}
