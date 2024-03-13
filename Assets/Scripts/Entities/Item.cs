using UnityEngine;

public class Item : Entity
{
    [SerializeField] private ItemScriptableObject _scriptableObject;

    public new ItemScriptableObject ScriptableObject
    {
        get { return _scriptableObject; }
    }

    public void Use()
    {
        switch (_scriptableObject.Type)
        {
            case EntityType.Key:
                Player.Inventory.Remove(this);
                Debug.Log("Key used.");

                break;
            case EntityType.VideoTape:
                if (InteractionController.Instance.CurrentInteraction != InteractionType.TV)
                    return;

                Player.Inventory.Remove(this);
                Debug.Log("Tape inserted.");

                break;
            case EntityType.Note:
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