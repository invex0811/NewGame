using UnityEngine;

public class Document : Entity
{
    [SerializeField] private DocumentScriptableObject _documentScriptableObject;
    public DocumentScriptableObject DocumentScriptableObject => _documentScriptableObject;

    public void Read()
    {
        switch (_documentScriptableObject.Type)
        {
            case DocumentType.Note:
                DocumentInspectorController.Instance.enabled = true;
                DocumentInspectorController.Instance.Initialize(_documentScriptableObject.Text);

                break;
        }
    }
    public override void Interact()
    {
        switch (EntityScriptableObject.Type)
        {
            case EntityType.Note:
                Player.Journal.Add(this);
                Destroy(gameObject);

                break;
            default:
                Destroy(gameObject);

                break;
        }
    }
}

public enum DocumentType
{
    Note
}