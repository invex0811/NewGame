using UnityEngine;

[System.Serializable]
class Note : Item
{
    private readonly int _id;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly Sprite _sprite;
    private readonly GameObject _prefab;

    public override int ID => _id;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override Sprite Sprite => _sprite;
    public override GameObject Prefab => _prefab;

    public Note(int id, string displayName, string description, string raycastFeedbackText, Sprite icon, GameObject prefab)
    {
        _id = id;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _sprite = icon;
        _prefab = prefab;
    }

    public override void Use()
    {
        Debug.Log("Note read.");
    }
    public override bool Interact()
    {
        Player.Inventory.Add(this);

        return true;
    }
}