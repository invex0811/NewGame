using UnityEngine;

[System.Serializable]
abstract class Item : Entity
{
    public abstract Sprite Sprite { get; }
    public abstract void Use();
}