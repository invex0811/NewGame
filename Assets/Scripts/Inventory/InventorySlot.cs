[System.Serializable]
public class InventorySlot
{
    public Item Item { get; private set; }

    public InventorySlot(Item item)
    {
        Item = item;
    }
}