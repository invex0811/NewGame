using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public int ID;
    public Transform InteractionPoint;
    public InteractionType InteractionType;
}

public enum InteractionType
{
    None,
    TV
}