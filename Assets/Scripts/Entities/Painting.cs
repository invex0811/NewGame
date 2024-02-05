using UnityEngine;

class Painting : Entity
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

    public Painting(int id, string displayName, string description, string raycastFeedbackText , GameObject prefab)
    {
        _id = id;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
    }

    public override bool Interact()
    {
        InteractionController.Instance.InspectEntity(ID);

        return false;
    }
}