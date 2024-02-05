using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

class TV : Entity
{

    private int _id;
    private string _displayName;
    private string _description;
    private string _raycastFeedbackText;
    private GameObject _prefab;

    public override int ID => _id;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override GameObject Prefab => _prefab;

    public TV(int id, string displayName, string description, string raycastFeedbackText, GameObject prefab)
    {
        _id = id;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
    }

    public override bool Interact()
    {
        Transform point = InteractionPoints.Instance.TV.transform;
        InteractionController.Instance.InteractionPoint = point;
        InteractionController.Instance.CurrentInteraction = InteractionType.TV;

        PlayerController.Instance.OpenInventory();
        PlayerController.Instance.enabled = false;
        InteractionController.Instance.MoveToInteractionPoint();

        return false;
    }
}