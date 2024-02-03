using UnityEngine;

[System.Serializable]
abstract class Item
{
    public abstract string DisplayName { get; }
    public abstract string Description { get; }
    public abstract Sprite Sprite { get; }
    public abstract GameObject Prefab { get; }
    public abstract void Use();
    public abstract void PickUp();
}