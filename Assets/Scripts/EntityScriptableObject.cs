using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Entity")]
public class EntityScriptableObject : ScriptableObject
{
    [SerializeField] private EntityType _type;
    [SerializeField] private string _displayName;
    [SerializeField] private string _description;
    [SerializeField] private string _raycastFeedbackText;
    [SerializeField] private GameObject _prefab;

    public EntityType Type => _type;
    public string DisplayName => _displayName;
    public string Description => _description;
    public string RaycastFeedbackText => _raycastFeedbackText;
    public GameObject Prefab => _prefab;
}