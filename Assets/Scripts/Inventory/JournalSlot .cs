[System.Serializable]
public class JournalSlot
{
    public Document Document { get; private set; }

    public JournalSlot(Document document)
    {
        Document = document;
    }
}