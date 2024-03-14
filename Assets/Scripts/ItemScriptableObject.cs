using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private ItemType _type;
    [SerializeField] private string _description;
    [SerializeField] private string _raycastFeedbackText;
    [SerializeField] private Sprite _sprite;

    public ItemType Type => _type;
    public string Description => _description;
    public string RaycastFeedbackText => _raycastFeedbackText;
    public Sprite Sprite => _sprite;
}