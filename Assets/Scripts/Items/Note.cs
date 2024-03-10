using UnityEngine;

[System.Serializable]
class Note : Document
{
    private readonly TypesOfEntity _type;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly GameObject _prefab;
    private readonly string _text;

    public override TypesOfEntity Type => _type;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override GameObject Prefab => _prefab;
    public override string Text => _text;

    public Note(TypesOfEntity type, string displayName, string description, string raycastFeedbackText, GameObject prefab, string textAsset)
    {
        _type = type;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
        _text = textAsset;
    }

    public override void Read()
    {
        DocumentInspectorController.Instance.enabled = true;
        DocumentInspectorController.Instance.Initialize(_text);
    }
    public override void Interact(GameObject obj)
    {
        Player.Journal.Add(this);

        Object.Destroy(obj);
    }

    public override void StopInteraction()
    {
        throw new System.NotImplementedException();
    }
}