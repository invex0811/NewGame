using UnityEngine;

class TV : Entity
{
    private readonly int _id;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly GameObject _prefab;

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

    public override void Interact(GameObject obj)
    {
        GameManager.PauseGame();

        Transform point = InteractionPoints.Instance.TV.transform;
        InteractionController.Instance.InteractionPoint = point;
        InteractionController.Instance.CurrentInteraction = InteractionType.TV;
        InteractionController.Instance.MoveToInteractionPoint();

        InteractionController.Instance.OnStopInteraction += StopInteraction;

        UIController.Instance.OpenInventory();
        PlayerController.Instance.enabled = false;
    }

    public override void StopInteraction()
    {
        InteractionController.Instance.OnStopInteraction -= StopInteraction;

        InteractionController.Instance.ReturnToInteractionPosition();
        InteractionController.Instance.CurrentInteraction = InteractionType.None;

        GameManager.ResumeGame();
    }
}