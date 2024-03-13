using UnityEngine;

[System.Serializable]
class Key : Item
{
    private readonly EntityType _type;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly Sprite _sprite;
    private readonly GameObject _prefab;

    public override EntityType Type => _type;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override Sprite Sprite => _sprite;
    public override GameObject Prefab => _prefab;

    public Key(EntityType type, string displayName, string description,string raycastFeedbackText, Sprite icon, GameObject prefab)
    {
        _type = type;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _sprite = icon;
        _prefab = prefab;
    }

    public override void Use()
    {
        Player.Inventory.Remove(this);
        Debug.Log("Key used.");
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