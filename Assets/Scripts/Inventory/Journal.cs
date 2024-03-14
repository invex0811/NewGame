using System;
using System.Collections.Generic;

class Journal
{
    public List<JournalSlot> Slots { get; private set; } = new List<JournalSlot>();

    public void Add(Document document)
    {
        if (Slots.Count >= 30)
            return;

        JournalSlot newSlot = new(document);
        Slots.Add(newSlot);

        OnSlotsChanged?.Invoke();
    }
    public void Remove(Document document)
    {
        int index = Slots.IndexOf(Slots.Find(s => s.Document == document));

        if (Slots[index] != null)
            Slots.Remove(Slots[index]);

        OnSlotsChanged?.Invoke();
    }

    public event Action OnSlotsChanged;
}