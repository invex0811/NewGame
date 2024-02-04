using System.Collections.Generic;
using UnityEngine;

class Inventory
{
    public List<InventorySlot> Slots { get; private set; } = new List<InventorySlot>();

    public void Add(Item item)
    {
        if (Slots.Count >= 30)
            return;

        InventorySlot newSlot = new(ItemsList.GetID(item));
        Slots.Add(newSlot);
    }

    public void Remove(Item item)
    {
        int index = Slots.IndexOf(Slots.Find(s => s.ItemID == ItemsList.GetID(item)));

        if (Slots[index] != null)
            Slots.Remove(Slots[index]);
    }
}