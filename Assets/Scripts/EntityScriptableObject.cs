using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Entity")]
public class EntityScriptableObject : ScriptableObject
{
    [SerializeField] private EntityType _type;
    [SerializeField] private string _name;
    [SerializeField] private string _raycastFeedbackText;
    [SerializeField] private GameObject _prefab;

    public EntityType Type => _type;
    public string Name => _name;
    public string RaycastFeedbackText => _raycastFeedbackText;
    public GameObject Prefab => _prefab;
}