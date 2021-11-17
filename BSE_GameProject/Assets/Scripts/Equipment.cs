using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int meleeDamageModifier;
    public int magicDamageModifier;
    public int critChanceModifier;
    public int critDamageModifier;
    public int speedModifier;
    public int staminaRegenModifier;
    public int manaRegenModifier;

    public bool StatsApplied = false;


    


}

public enum EquipmentSlot { Weapon, Drink, Legs, Feather1, Feather2, Extra }