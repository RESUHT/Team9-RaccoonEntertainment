﻿using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);
        //add to inventory
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
