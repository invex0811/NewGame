using UnityEngine;

public class Document : Entity
{
    [SerializeField] private DocumentScriptableObject _scriptableObject;

    public new DocumentScriptableObject ScriptableObject => _scriptableObject;

    public void Read()
    {
        switch (_scriptableObject.Type)
        {
            case EntityType.Key:
                break;
            case EntityType.VideoTape:
                break;
            case EntityType.Note:
                DocumentInspectorController.Instance.enabled = true;
                DocumentInspectorController.Instance.Initialize(_scriptableObject.Text);

                break;
            case EntityType.Painting:
                break;
            case EntityType.TV:
                break;
            case EntityType.Door:
                break;
            case EntityType.SafeDigital:
                break;
            case EntityType.SafePadlock:
                break;
            default:
                break;
        }
    }
}