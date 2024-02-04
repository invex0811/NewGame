using UnityEngine;
[System.Serializable]
class Key : Item
{
    private string _displayName;
    private string _description;
    private Sprite _sprite;
    private GameObject _prefab;

    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override Sprite Sprite => _sprite;
    public override GameObject Prefab => _prefab;

    public Key(string displayName, string description, Sprite icon, GameObject prefab)
    {
        _displayName = displayName;
        _description = description;
        _sprite = icon;
        _prefab = prefab;
    }

    public override void Use()
    {
        Player.Inventory.Remove(this);
        Debug.Log("Key used.");
    }
    public override void PickUp()
    {
        Player.Inventory.Add(this);
    }
}