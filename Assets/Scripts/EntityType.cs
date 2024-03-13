using UnityEngine;

public class EntityID : MonoBehaviour
{
    [SerializeField] private EntityType _type;

    public EntityType Type
    {
        get => _type;
    }
}