using UnityEngine;

[System.Serializable]
abstract class Entity
{
    public abstract int ID { get; }
    public abstract string DisplayName { get; }
    public abstract string Description { get; }
    public abstract string RaycastFeedbackText { get; }
    public abstract GameObject Prefab { get; }

    public abstract void Interact(GameObject obj);
    public abstract void StopInteraction();
}