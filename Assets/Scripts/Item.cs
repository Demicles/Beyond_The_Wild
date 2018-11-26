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
}
