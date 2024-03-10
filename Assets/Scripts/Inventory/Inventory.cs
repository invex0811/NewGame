using System;
using System.Collections.Generic;

class Inventory
{
    public List<Slot> Slots { get; private set; } = new List<Slot>();

    public void Add(Item item)
    {
        if (Slots.Count >= 30)
            return;

        Slot newSlot = new(item.Type);
        Slots.Add(newSlot);

        OnSlotsChanged?.Invoke();
    }
    public void Remove(Item item)
    {
        int index = Slots.IndexOf(Slots.Find(s => s.Type == item.Type));

        if (Slots[index] != null)
            Slots.Remove(Slots[index]);

        OnSlotsChanged?.Invoke();
    }

    public event Action OnSlotsChanged;
}