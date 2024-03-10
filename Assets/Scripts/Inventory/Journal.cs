using System;
using System.Collections.Generic;

class Journal
{
    public List<Slot> Slots { get; private set; } = new List<Slot>();

    public void Add(Document document)
    {
        if (Slots.Count >= 30)
            return;

        Slot newSlot = new(document.Type);
        Slots.Add(newSlot);

        OnSlotsChanged?.Invoke();
    }
    public void Remove(Document document)
    {
        int index = Slots.IndexOf(Slots.Find(s => s.Type == document.Type));

        if (Slots[index] != null)
            Slots.Remove(Slots[index]);

        OnSlotsChanged?.Invoke();
    }

    public event Action OnSlotsChanged;
}