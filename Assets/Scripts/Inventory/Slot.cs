[System.Serializable]
public class Slot
{
    public TypesOfEntity Type { get; private set; }

    public Slot(TypesOfEntity type)
    {
        Type = type;
    }
}