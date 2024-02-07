using UnityEngine;

public class EntityID : MonoBehaviour
{
    [SerializeField] private int _id;

    public int ID
    {
        get => _id;
    }
}