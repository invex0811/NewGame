using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : EntityScriptableObject
{
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => _sprite;
}