using UnityEngine;

class VideoTape : Item
{
    private string _displayName;
    private string _description;
    private Sprite _sprite;
    private GameObject _prefab;

    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override Sprite Sprite => _sprite;
    public override GameObject Prefab => _prefab;

    public VideoTape(string displayName, string description, Sprite icon, GameObject prefab)
    {
        _displayName = displayName;
        _description = description;
        _sprite = icon;
        _prefab = prefab;
    }

    public override void Use()
    {
        if (InteractionController.Instance.CurrentInteraction != InteractionType.TV)
            return;

        Player.Inventory.Remove(this);
        Debug.Log("Tape inserted.");
    }
    public override void PickUp()
    {
        Player.Inventory.Add(this);
    }
}