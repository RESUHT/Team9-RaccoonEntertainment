using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public float meleeDamageModifier;
    public float magicDamageModifier;
    public float critChanceModifier;
    public float critDamageModifier;
    public float speedModifier;
    public float staminaRegenModifier;
    public float manaRegenModifier;




}

public enum EquipmentSlot { Weapon, Drink, Legs, Feather1, Feather2 }