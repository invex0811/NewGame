[System.Serializable]
public class Slot
{
    public EntityType Type { get; private set; }

    public Slot(EntityType type)
    {
        Type = type;
    }
}