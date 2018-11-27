using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {

    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int baseEXP = 1000;
    public int maxLevel = 100;

    public int currentHP;
    public int currentMP;
    public int maxHP = 100;
    public int maxMP = 30;
    public int[] mpLvlBonus;

    public int strenght;
    public int defence;
    public int weaponPower;
    public int armorPower;

    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;

    // Use this for initialization
    void Start () {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // TODO remove this condition; only for debug purposes
        if (Input.GetKeyDown(KeyCode.K))
        {
            addExp(1000);
        }
    }

    // Adding XP and leveling characters
    public void addExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];

                playerLevel++;

                // Determine whether to add to STR or DEF based on odd/even
                if (playerLevel % 2 == 0)
                {
                    strenght++;
                }
                else
                {
                    defence++;
                }

                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMP += mpLvlBonus[playerLevel];
                currentMP = maxMP;
            }
        }

        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
