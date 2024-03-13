using UnityEngine;

class SafePadlock : Entity
{
    private readonly EntityType _type;
    private readonly string _displayName;
    private readonly string _description;
    private readonly string _raycastFeedbackText;
    private readonly GameObject _prefab;

    private GameObject _currentObject;

    public override EntityType Type => _type;
    public override string DisplayName => _displayName;
    public override string Description => _description;
    public override string RaycastFeedbackText => _raycastFeedbackText;
    public override GameObject Prefab => _prefab;

    public SafePadlock(EntityType type, string displayName, string description, string raycastFeedbackText, GameObject prefab)
    {
        _type = type;
        _displayName = displayName;
        _description = description;
        _raycastFeedbackText = raycastFeedbackText;
        _prefab = prefab;
    }

    public override void Interact(GameObject obj)
    {
        _currentObject = obj;
        GameManager.PauseGame();
        GameManager.ChangeTypeOfControll(TypesOfControl.InteractionControl);

        Transform point = InteractionPoints.Instance.SafePadlock.transform;
        InteractionController.Instance.InteractionPoint = point;
        InteractionController.Instance.CurrentInteraction = InteractionType.SafePadlock;
        InteractionController.Instance.MoveToInteractionPoint();

        InteractionController.Instance.OnStopInteraction += StopInteraction;

        PlayerController.Instance.enabled = false;
        CameraController.Instance.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _currentObject.GetComponentInChildren<Padlock>().enabled = true;
    }
    public override void StopInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;

        InteractionController.Instance.OnStopInteraction -= StopInteraction;

        InteractionController.Instance.CurrentInteraction = InteractionType.None;
        InteractionController.Instance.ReturnToInteractionPosition();

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.ResumeGame();

        _currentObject = null;
    }
}