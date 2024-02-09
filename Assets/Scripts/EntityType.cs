using UnityEngine;

public class EntityID : MonoBehaviour
{
    [SerializeField] private TypesOfEntity _type;

    public TypesOfEntity Type
    {
        get => _type;
    }
}