using UnityEngine;
[System.Serializable]

class Key : Item
{
    private int _id;
    private string _displayName;
    private string _description;
    private string _raycastFeedbackText;
    private Sprite _sprite;
    private GameObject _prefab;

    public override int ID => _id;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override Sprite Sprite => _sprite;
    public override GameObject Prefab => _prefab;

    public Key(int id, string displayName, string description,string raycastFeedbackText, Sprite icon, GameObject prefab)
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
        Player.Inventory.Remove(this);
        Debug.Log("Key used.");
    }
    public override bool Interact()
    {
        Player.Inventory.Add(this);

        return true;
    }
}