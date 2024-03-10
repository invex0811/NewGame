[System.Serializable]
abstract class Document : Entity
{
    public abstract string Text { get; }

    public abstract void Read();
}