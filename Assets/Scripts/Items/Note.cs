using UnityEngine;

[System.Serializable]
class Note : Item
{
    private readonly TypesOfEntity _type;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly Sprite _sprite;
    private readonly GameObject _prefab;
    private readonly string _text;

    public override TypesOfEntity Type => _type;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override Sprite Sprite => _sprite;
    public override GameObject Prefab => _prefab;

    public string Text => _text;

    public Note(TypesOfEntity type, string displayName, string description, string raycastFeedbackText, Sprite icon, GameObject prefab, string textAsset)
    {
        _type = type;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _sprite = icon;
        _prefab = prefab;
        _text = textAsset;
    }

    public override void Use()
    {
        DocumentsPanelController.Instance.enabled = true;
        DocumentsPanelController.Instance.Initialize(_text);
    }
    public override void Interact(GameObject obj)
    {
        Player.Inventory.Add(this);

        Object.Destroy(obj);
    }

    public override void StopInteraction()
    {
        throw new System.NotImplementedException();
    }
}