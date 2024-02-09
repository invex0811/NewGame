[System.Serializable]
public class InventorySlot
{
    public TypesOfEntity Type { get; private set; }

    public InventorySlot(TypesOfEntity type)
    {
        Type = type;
    }
}