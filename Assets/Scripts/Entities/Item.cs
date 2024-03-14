using UnityEngine;

public class Item : Entity
{
    [SerializeField] private ItemScriptableObject _itemScriptableObject;
    public ItemScriptableObject ItemScriptableObject => _itemScriptableObject;

    public Item (ItemScriptableObject scriptableObject)
    {
        _itemScriptableObject = scriptableObject;
    }

    public void Use()
    {
        switch (_itemScriptableObject.Type)
        {
            case ItemType.Key:
                Player.Inventory.Remove(this);
                Debug.Log("Key used.");

                break;
            case ItemType.VideoTape:
                if (InteractionController.Instance.CurrentInteraction != InteractionType.TV)
                    return;

                Player.Inventory.Remove(this);
                Debug.Log("Tape inserted.");

                break;
            default:
                Player.Inventory.Remove(this);
                Debug.Log($"{EntityScriptableObject.Name} used");

                break;
        }
    }
    public override void Interact()
    {
        switch (EntityScriptableObject.Type)
        {
            case EntityType.Key:
                Player.Inventory.Add(this);
                Destroy(gameObject);

                break;
            case EntityType.VideoTape:
                Player.Inventory.Add(this);
                Destroy(gameObject);

                break;
            default:
                Destroy(gameObject);

                break;
        }
    }
}

public enum ItemType
{
    Key,
    VideoTape
}