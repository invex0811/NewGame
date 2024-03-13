[System.Serializable]
public class Slot
{
    public Entity Entity { get; private set; }

    public Slot(Entity entity)
    {
        Entity = entity;
    }
}