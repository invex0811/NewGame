using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public int ItemID { get; private set; }

    public InventorySlot(int ID)
    {
        ItemID = ID;
    }
}