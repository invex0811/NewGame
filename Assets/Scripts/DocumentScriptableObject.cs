using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Document")]
public class DocumentScriptableObject : ScriptableObject
{
    [SerializeField] private DocumentType _type;
    [SerializeField] private string _text;
    [SerializeField] private string _raycastFeedbackText;

    public DocumentType Type => _type;
    public string Text => _text;
    public string RaycastFeedbackText => _raycastFeedbackText;
}