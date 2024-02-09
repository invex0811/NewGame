using UnityEngine;

class Painting : Entity
{
    private readonly TypesOfEntity _type;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly GameObject _prefab;

    public override TypesOfEntity Type => _type;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override GameObject Prefab => _prefab;

    public Painting(TypesOfEntity type, string displayName, string description, string raycastFeedbackText , GameObject prefab)
    {
        _type = type;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
    }

    public override void Interact(GameObject obj)
    {
        GameManager.ChangeTypeOfControll(TypesOfControl.InspectionControll);
        InspectionController.Instance.enabled = true;
        InspectionController.Instance.Initialize(Type);
    }
    public override void StopInteraction()
    {
        throw new System.NotImplementedException();
    }
}